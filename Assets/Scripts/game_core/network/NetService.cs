using DefaultNamespace;
using UnityEngine;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using System.Collections.Generic;
using System.IO;
using Game.Core.Network;
using Google.Protobuf.WellKnownTypes;
using Protobuf;

namespace game_core
{
    public class NetService : AbstractModule
    {
        private ClientWebSocket ws;
        private CancellationTokenSource cts;
        private string serverUrl = "ws://127.0.0.1:8000/ws/test"; // 替换为你的WebSocket服务器地址
        
        private Dictionary<string, Action<ResponseMessage>> messageHandlers = new Dictionary<string, Action<ResponseMessage>>(); // 存储消息处理函数的字典
        private Dictionary<string, Action<ResponseMessage>> s2cHandlers = new Dictionary<string, Action<ResponseMessage>>();

        public override void initialize()
        {
            base.initialize();
            ConnectToServer();
        }

        public override void start()
        {
            base.start();
            SubscribeS2C<User>("/test", (message) =>
            {
                Debug.Log("S2C message:" + message.Age + message.Name);
            });
        }


        // 修改 ConnectToServer 方法
        private async void ConnectToServer()
        {
            try
            {
                ws = new ClientWebSocket();
                cts = new CancellationTokenSource();
                await ws.ConnectAsync(new Uri(serverUrl), cts.Token);

                // 连接成功后启动接收循环
                _ = ReceiveLoop(); // 使用 _ = 来显式忽略 Task
            }
            catch (Exception ex)
            {
                Debug.LogError($"连接WebSocket时发生错误: {ex.Message}");
            }
        }

        // 接收消息循环
        private async Task ReceiveLoop()
        {
            // 使用可增长的内存流来存储消息
            using (MemoryStream messageStream = new MemoryStream())
            {
                byte[] buffer = new byte[4096]; // 4KB的接收缓冲区

                while (ws.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = null;

                    try
                    {
                        do
                        {
                            // 接收数据片段
                            result = await ws.ReceiveAsync(
                                new ArraySegment<byte>(buffer),
                                cts.Token
                            );

                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                await ws.CloseAsync(
                                    WebSocketCloseStatus.NormalClosure,
                                    "关闭连接",
                                    CancellationToken.None
                                );
                                return;
                            }

                            // 将收到的数据写入内存流
                            await messageStream.WriteAsync(buffer, 0, result.Count);
                        } while (!result.EndOfMessage); // 继续接收直到消息结束

                        // 消息接收完成，处理完整消息
                        byte[] completeMessage = messageStream.ToArray();
                        ResponseMessage response = ResponseMessage.Parser.ParseFrom(completeMessage);
                        if (response.Type == RequestType.ClientToServer)
                        {
                            Debug.Log($"Received message: {response.Path}");
                            messageHandlers.TryGetValue(response.ResponseId, out Action<ResponseMessage> handler);
                            handler?.Invoke(response);
                            messageHandlers.Remove(response.ResponseId);
                        }
                        else if (response.Type == RequestType.ServerToClient)
                        {
                            s2cHandlers.TryGetValue(response.Path, out Action<ResponseMessage> handler);
                            handler?.Invoke(response);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"接收消息错误: {e.Message}");
                        break;
                    }
                }
            }
        }

        // 发送消息的新方法，需要包含消息类型
        public async Task SendMessageAsync<T>(RequestMessage message, Action<T> callback) where T : IMessage,new()
        {
            if (ws.State != WebSocketState.Open)
                return;
            messageHandlers.Add(message.RequestId,handleMessage(callback));
            // 计算消息体的大小
            int messageSize = message.CalculateSize();
            Byte[] buffer = new Byte[messageSize];
            message.WriteTo(buffer);
            await ws.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Binary,
                true,
                cts.Token
            );
        }

        public async Task SendRequest<T>(string path, IMessage param, Action<T> callback) where T : IMessage, new()
        {
            RequestMessage request = new RequestMessage();
            request.RequestId = RequestIdGenerator.Generate();
            request.Type = RequestType.ClientToServer;
            request.Path = path;
            request.Body = Any.Pack(param);
            SendMessageAsync(request, callback);
        }

        public Action SubscribeS2C<T>(string url, Action<T> callback) where T : IMessage, new()
        {
            s2cHandlers.Add(url,handleMessage(callback));
            return () =>
            {
                s2cHandlers.Remove(url);
            };
        }

        private void OnDestroy()
        {
            CloseConnection();
        }

        private Action<ResponseMessage> handleMessage<T>(Action<T> handler) where T : IMessage, new()
        {
            return (response) =>
            {
                handler(response.Body.Unpack<T>());
            };
        }

        // 关闭连接
        private async void CloseConnection()
        {
            if (ws != null && ws.State == WebSocketState.Open)
            {
                await ws.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "应用关闭",
                    CancellationToken.None
                );
            }

            cts?.Cancel();
            cts?.Dispose();
        }
        
    }
}