using building;
using character.attribute;
using DefaultNamespace;
using UnityEngine;

namespace affiliation
{
    public class BlueAffiliation : Affiliation
    {

        private BuildingFactory _buildingFactory = new BuildingFactory();
        private Home _home;
        
        public BlueAffiliation()
        {
            affiliation = AffiliationEnum.BLUE;
        }

        public override void initialize()
        {
            BuildingAttribute buildingAttribute = new BuildingAttribute();
            buildingAttribute.setOrAddAttribute(new HealthPoints(100,100,0));
            _home = _buildingFactory.createHome(affiliation, new Vector3((float)-110.37,17,0),buildingAttribute);
        }

        public override void generateSolider(string prefabName)
        {
            _home.createUnit(prefabName);
        }


        public override void start()
        {
            base.start();
        }
    }
}