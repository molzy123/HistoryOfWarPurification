using System;
using System.Collections.Generic;
using DefaultNamespace;
using Factory;
using UnityEngine;

namespace character.solider
{
    public class SoliderManager : AbstractModule
    {
        private Dictionary<AffiliationEnum,List<Solider>> _soliders;
        private Dictionary<Guid,Solider> _soliderMap;

        private SoliderFactory _blueSoliderFactory;
        private SoliderFactory _redSoliderFactory;
        
        public SoliderManager()
        {
            _soliders = new Dictionary<AffiliationEnum, List<Solider>>();
            _soliderMap = new Dictionary<Guid, Solider>();
        }
        
        public void addSolider(Solider solider)
        {
            if (!_soliders.ContainsKey(solider.affiliation))
            {
                _soliders.Add(solider.affiliation, new List<Solider>());
            }
            _soliders[solider.affiliation].Add(solider);
            _soliderMap.Add(solider.id, solider);
        }
        
        public void removeSolider(Solider solider)
        {
            if (!_soliders.ContainsKey(solider.affiliation)) return;
            _soliders[solider.affiliation].Remove(solider);
            _soliderMap.Remove(solider.id);
        }
        
        public List<Solider> getSoliderByAffiliation(AffiliationEnum affiliationEnum)
        {
            return _soliders[affiliationEnum];
        }
        
        public Solider getSoliderByGameObject(Guid id)
        {
            if (!_soliderMap.ContainsKey(id)) return null;
            return _soliderMap[id];
        }
        
    }
}