using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 100f; // 摄像机移动速度
    public float zoomSpeed = 1f; // 缩放速度
    public float maxVision = -20f;
    public float minVision = -50f ;
    public float minX, maxX; // 地图边界限制
    public float edgeThreshold = 25f; // 边缘阈值

    void Update()
    {
        // 获取鼠标滚轮滚动的输入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            ZoomCamera(scrollInput * 10 * zoomSpeed);
        }
        
        // 缩放摄像机
        ZoomCamera(scrollInput);

        // 获取鼠标的屏幕位置
        Vector3 mousePosition = Input.mousePosition;

        // 计算摄像机的目标位置
        Vector3 targetPosition = transform.position;

        // 根据鼠标接近屏幕边缘的位置移动摄像机
        if (mousePosition.x < edgeThreshold)
        {
            targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (mousePosition.x > Screen.width - edgeThreshold)
        {
            targetPosition += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // 限制摄像机在地图边界内移动
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        // 应用新的位置
        transform.position = targetPosition;
    }

    void ZoomCamera(float scrollInput)
    {
        Vector3 originPos = gameObject.transform.position;
        originPos.z = Math.Clamp(originPos.z + scrollInput, minVision, maxVision);
        gameObject.transform.position = originPos;
    }
}