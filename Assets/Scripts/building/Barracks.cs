
using affiliation;
using character;
using character.attribute;
using character.solider;
using DefaultNamespace;
using Factory;
using UnityEngine;

namespace building
{
    public class Barracks : Building
    {
        public Vector3 spawnPoint { get; set; }
        public AffiliationEnum affiliation { get; set; }
        private SoliderDirector _soliderDirector;
        
        public Barracks()
        {
            SoliderBuilder soliderBuilder = new SoliderBuilder(affiliation, "Chara_00");
            _soliderDirector = new SoliderDirector(soliderBuilder);
        }
        
        public void createUnit(string prefabName)
        {
            Solider solider = _soliderDirector.construct();
        }
        
        public void upgrade()
        {
            
        }
        
        public void destroy()
        {
        }


        
    }
}