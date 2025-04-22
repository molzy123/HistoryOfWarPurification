using affiliation;
using character;
using character.attribute;
using character.solider;
using DefaultNamespace;
using UI;
using UnityEngine;

namespace Factory
{
    public class SoliderFactory : ResourcesFactory
    {
        private AffiliationEnum _affiliationEnum;
        
        public SoliderFactory(AffiliationEnum affiliationEnum)
        {
            _affiliationEnum = affiliationEnum;
        }
        
        public Solider createPlayerSolider(string prefabName)
        {
            GameObject obj = loadGameObject(prefabName);
            Solider solider = new Solider()
            {
                affiliation = _affiliationEnum,
            };
            solider.setSoliderUI(createSoliderUI(solider));
            Locator.fetch<SoliderManager>().addSolider(solider);
            return solider;
        }

        public SoliderUI createSoliderUI(Solider solider)
        {
            GameObject ui = loadGameObject("SolidersUI");
            SoliderUI soliderUI = new SoliderUI();
            soliderUI.follow(solider.GetComponent<Transform>());
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            ui.transform.SetParent(canvas.transform, false);
            soliderUI.initialize();
            return soliderUI;
        }
    }
    
    
}