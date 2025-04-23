using DefaultNamespace.map;
using ui.frame;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudView : ViewBase
    {
        [Binding] Button _btnCube { get; set; }
        [Binding] Button _btnSquare { get; set; }

        private GameObject _currentCube; // 当前创建的cube引用
        private bool _isPlacingCube = false; // 是否正在放置cube
        private GridCell _currentGridCell; // 当前鼠标指向的GridCell

        // 定义用于GridCell检测的层级掩码
        private LayerMask _gridLayerMask;

        protected override void onShow()
        {
            base.onShow();
            onClick(_btnCube, createCube);
            onClick(_btnSquare, createSquare);

            // 假设GridCell在"Grid"层，如果没有特定层级，可以设置为默认层级
            _gridLayerMask = LayerMask.GetMask("Grid");

            // 如果没有特定的"Grid"层，可以使用默认层级
            if (_gridLayerMask.value == 0)
            {
                _gridLayerMask = LayerMask.GetMask("Default");
            }
        }

        private void createCube()
        {
            // 如果已经在放置cube，则不重复创建
            if (_isPlacingCube)
                return;

            // 创建一个cube
            _currentCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _currentCube.name = "FollowingCube";

            // 将创建的cube放在一个不会被射线检测到的层级上
            // 假设我们有一个名为"PlacingObjects"的层级
            int placingLayer = LayerMask.NameToLayer("PlacingObjects");
            if (placingLayer != -1)
            {
                _currentCube.layer = placingLayer;
            }
            else
            {
                // 如果没有特定的层级，可以创建一个忽略射线的物理材质
                Collider cubeCollider = _currentCube.GetComponent<Collider>();
                if (cubeCollider != null)
                {
                    // 禁用碰撞器，使射线能够穿过
                    cubeCollider.enabled = false;
                }
            }

            // 设置初始位置，y坐标为1
            _currentCube.transform.position = new Vector3(0, 1, 0);

            // 标记为正在放置cube状态
            _isPlacingCube = true;
        }

        private void createSquare()
        {
            Debug.Log("OnClickSave");
        }

        private void Update()
        {
            // 获取鼠标指向的GridCell
            UpdateCurrentGridCell();

            // 如果正在放置cube，让cube跟随鼠标移动
            if (_isPlacingCube && _currentCube != null)
            {
                // 如果找到了GridCell，将cube放在GridCell的位置上
                if (_currentGridCell != null)
                {
                    // 获取GridCell的位置，并将y坐标设为1
                    Vector3 cellPosition = _currentGridCell.transform.position;
                    _currentCube.transform.position = new Vector3(cellPosition.x, 1, cellPosition.z);
                }
                else
                {
                    // 如果没有找到GridCell，使用之前的射线检测方法
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Plane plane = new Plane(Vector3.up, new Vector3(0, 0, 0));

                    if (plane.Raycast(ray, out float distance))
                    {
                        Vector3 hitPoint = ray.GetPoint(distance);
                        _currentCube.transform.position = new Vector3(hitPoint.x, 1, hitPoint.z);
                    }
                }

                // 如果点击鼠标左键，完成放置
                if (Input.GetMouseButtonDown(0))
                {
                    // 放置完成后，恢复碰撞器
                    Collider cubeCollider = _currentCube.GetComponent<Collider>();
                    if (cubeCollider != null)
                    {
                        cubeCollider.enabled = true;
                    }

                    _isPlacingCube = false;
                    _currentCube = null;
                }
            }
        }

        private void UpdateCurrentGridCell()
        {
            // 重置当前GridCell
            _currentGridCell = null;

            // 从摄像机发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, _gridLayerMask);

            // 遍历所有击中的物体
            foreach (RaycastHit hit in hits)
            {
                // 尝试获取击中物体上的GridCell组件
                GridCell gridCell = hit.collider.GetComponent<GridCell>();

                // 如果找到了GridCell组件
                if (gridCell != null)
                {
                    _currentGridCell = gridCell;
                    Debug.Log($"当前指向的GridCell: {gridCell.transform.position.x} {gridCell.transform.position.z}");
                    break; // 找到第一个GridCell后退出循环
                }
            }

            // 如果没有找到GridCell，可以尝试使用另一种方法
            if (_currentGridCell == null)
            {
                // 使用射线投射到一个固定的平面上，然后找最近的GridCell
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                if (groundPlane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);

                    // 找到距离hitPoint最近的GridCell
                    // 这需要有一个方法来获取所有的GridCell或者使用Physics.OverlapSphere
                    Collider[] nearbyColliders = Physics.OverlapSphere(hitPoint, 1.0f, _gridLayerMask);
                    float closestDistance = float.MaxValue;

                    foreach (Collider collider in nearbyColliders)
                    {
                        GridCell gridCell = collider.GetComponent<GridCell>();
                        if (gridCell != null)
                        {
                            float dist = Vector3.Distance(hitPoint, gridCell.transform.position);
                            if (dist < closestDistance)
                            {
                                closestDistance = dist;
                                _currentGridCell = gridCell;
                            }
                        }
                    }

                    if (_currentGridCell != null)
                    {
                        Debug.Log(
                            $"找到最近的GridCell: {_currentGridCell.transform.position.x} {_currentGridCell.transform.position.z}");
                    }
                }
            }
        }
    }
}