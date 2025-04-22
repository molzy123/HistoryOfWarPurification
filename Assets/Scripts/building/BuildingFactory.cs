using character.attribute;
using DefaultNamespace;
using Factory;
using UnityEngine;

namespace building
{
    public class BuildingFactory : ResourcesFactory
    {
        
        
        public Home createHome(AffiliationEnum affiliationEnum, Vector3 spawnPoint, BuildingAttribute buildingAttribute)
        {
            Home home = new Home()
            {
                affiliation = affiliationEnum,
                spawnPoint = spawnPoint,
                attributes = buildingAttribute
            };
            home.transform.localScale = spawnPoint;
            return home;
        }
        
    }
}