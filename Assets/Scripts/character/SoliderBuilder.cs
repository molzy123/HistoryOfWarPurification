using character.attribute;
using character.moving;
using character.solider;
using DefaultNamespace;
using game_core;
using UI;
using UnityEngine;

namespace character
{
    public class SoliderBuilder : ISoliderBuilder
    {
        private Solider _solider;
        private readonly AffiliationEnum _affiliationEnum;
        private string _prefabName;
        
        
        public SoliderBuilder(AffiliationEnum affiliationEnum, string prefabName)
        {
            _affiliationEnum = affiliationEnum;
            _prefabName = prefabName;
            _solider = new Solider();
        }
   
        public void setAffiliation()
        {
            _solider.affiliation = _affiliationEnum;
        }
        public void setSoliderUI()
        {
            GameObject gameObject = Locator.fetch<GameObjectUIService>().loadGameObjectUI("SolidersUI");
            SoliderUI soliderUI = gameObject.GetComponent<SoliderUI>();
            soliderUI.initialize();
            _solider.setSoliderUI(soliderUI);
        }
        public void setCharacterAttribute()
        {
            CharacterAttribute characterAttribute = new CharacterAttribute();
            characterAttribute.setOrAddAttribute(new HealthPoints(100,100,0));
            characterAttribute.setOrAddAttribute(new Speed(20,20,0));
            _solider.characterAttribute = characterAttribute;
        }
        
        public void setMoveController()
        {
            if (_solider.characterAttribute == null)
            {
                Debug.LogWarning("Please set the character attribute first!");
                return;
            }

            var direction = _solider.affiliation == AffiliationEnum.RED ? DirectionEnum.RIGHT : DirectionEnum.LEFT;
            var speed = _solider.characterAttribute.getAttribute(AttributeEnum.SPD);
            MovingController movingController = new MovingController( direction, speed, _solider.gameObject.transform);
            _solider.setMovingController(movingController);
        }
        
        public void executeInitialize() {
            _solider.initialize();
        }

        public void addSoliderToManager()
        {
            Locator.fetch<SoliderManager>().addSolider(_solider);
        }
        
        public Solider getSolider()
        {
            Solider result = _solider;
            _solider = new Solider();
            return result;
        }
        
    }
}