using building;
using DefaultNamespace;
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
        
        protected override void onShow()
        {
            base.onShow();
            onClick(_btnCube, createCube);
            onClick(_btnSquare, createSquare);
        }

        private void createCube()
        {
            Locator.fetch<BuildingManager>().building = true;
            // 如果已经在放置cube，则不重复创建
            if (_isPlacingCube)
                return;

            // 创建一个cube
            _currentCube = Locator.fetch<BuildingManager>().createBuilding(BuildingType.CUBE1);
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

                if (_currentGridCell != null)
                {
                    _currentCube.GetComponent<Renderer>().material.color = _currentGridCell.isSpace ? Color.white : Color.red;
                }

                // 如果点击鼠标左键，完成放置
                if (Input.GetMouseButtonDown(0))
                {
                    Building building = _currentCube.GetComponent<Building>();
                    if (_currentGridCell != null && _currentGridCell.canBuild(building))
                    {
                        _currentGridCell.build(_currentCube);
                        Locator.fetch<BuildingManager>().building = false;
                        _isPlacingCube = false;
                        _currentCube = null;
                    }
                    else
                    {
                        Debug.Log("无法在上面创创建建筑");
                    }
                }
            }
        }

        private void UpdateCurrentGridCell()
        {
            int layerMask = LayerMask.GetMask("Floor");
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, layerMask))
                {
                    GridCell gridCell = hit.collider.GetComponent<GridCell>();
                    if (gridCell != null)
                    {
                        // 设置当前GridCell
                        _currentGridCell = gridCell;
                    }
                }
            }
        }
    }
}