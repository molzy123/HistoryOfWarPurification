using System.Collections.Generic;
using System.IO;
using conf.Building;
using DefaultNamespace;
using game_core;
using Google.Protobuf.WellKnownTypes;
using UnityEditor.MPE;
using UnityEngine;

namespace building
{

    public enum BuildingType
    {
        CUBE1
    }
    
    public class BuildingManager : AbstractModule
    {
        
        private BuildingFactory _buildingFactory = new BuildingFactory();
        
        public override void initialize()
        {
            BuildingMgr.InitInstance(new FileInfo("Assets/ConfigData/Building.dat"));
            conf.Building.Building building = BuildingMgr.GetInstance().Get(1);
            Debug.Log(building.Name);
        }
        
        private bool _building;
        public bool building
        {
            get
            {
                return _building;
            }
            set
            {
                if (_building != value)
                {
                    EventManager.Dispatch(EventEnum.BUILDING_CHANGED, value);
                    _building = value;
                }
            }
        }

        public GameObject createBuilding(BuildingType type)
        {
            return _buildingFactory.createCube1();
        }


    }
}