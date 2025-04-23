using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.map
{
    public class GridMapManager: AbstractModule
    {
        
        private MapFactory _mapFactory;
        private GameObject _mapRoot;
        
        public override void initialize()
        {
            _mapFactory = new MapFactory();
            // 创建一个节点，放到场景中
            _mapRoot = new GameObject("MapRoot");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.MoveGameObjectToScene(_mapRoot, scene);
            
            _mapFactory.createGridCells(10, 10, _mapRoot);
        }

        public override void start()
        {
            
        }

        public override void OnDestroy()
        {
            Object.Destroy(_mapRoot);
        }

        public override void update()
        {
            
        }
    }
}