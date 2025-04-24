using System;
using game_core;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace.map
{
    public class GridMapManager: AbstractModule
    {
        
        private readonly MapFactory _mapFactory = new MapFactory();
        private readonly GameObject _mapRoot = new GameObject("MapRoot");
        private GridCell[,] _gridCells;
        
        public override void initialize()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.MoveGameObjectToScene(_mapRoot, scene);
            _gridCells = _mapFactory.createGridCells(10, 10, _mapRoot);
        }
        
        public override void start()
        {
            EventManager.AddListener(EventEnum.BUILDING_CHANGED, state =>
            {
                foreach (var gridCell in _gridCells)
                {
                    gridCell.updateState((bool)state);
                }
            });
        }

        public override void OnDestroy()
        {
            Object.Destroy(_mapRoot);
        }

    }
}