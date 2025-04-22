using affiliation;
using character;
using character.attribute;
using DefaultNamespace;
using UI;
using UnityEngine;

namespace building
{
    public class Home : Barracks
    {
        public BuildingAttribute attributes { get; set; }
        public AffiliationEnum affiliation { get; set; }
        
        public bool isLive()
        {
            return attributes.isLive();
        }
    }
}