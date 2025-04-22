using System.Text;
using DefaultNamespace;
using game_core;
using Game.Core.Network;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Protobuf;
using TMPro;
using ui.frame;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WebSocketTestView : ViewBase
    {
        [Binding("_txtContent")] TextMeshProUGUI _txtContent { get; set; }
        [Binding("_btnSend")] Button _btnSend { get; set; }
        [Binding("_input")] TMP_InputField _input { get; set; }

        private NetService _netService;

        protected override void onShow()
        {
            base.onShow();
            _netService = Locator.fetch<NetService>();

            _btnSend.onClick.AddListener(OnClickSend);
        }
        
        private void OnClickSend()
        {
            var user = new User()
            {
                Name = "林振宇",
                Age = 25,
            };
            // _ = _netService.SendRequest<User>("/test",user ,onSendSuccess);
            _ = _netService.SendMessageAsync<User>(new RequestMessage()
            {
                Path = "/test",
                Body = Any.Pack(user),
                Type = RequestType.ServerToClient,
                RequestId = RequestIdGenerator.Generate()
            }, message =>
            {
                Debug.Log("User Name: " + user.Name + " Age:" + user.Age);
            });
        }
        
        private void onSendSuccess(User user)
        {
            Debug.Log("User Name: " + user.Name + " Age:" + user.Age);

        }
    }
}