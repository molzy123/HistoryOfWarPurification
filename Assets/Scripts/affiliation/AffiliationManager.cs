using System;
using System.Collections.Generic;
using common;
using DefaultNamespace;
using Factory;
using UnityEngine.PlayerLoop;

namespace affiliation
{
    public class AffiliationManager : AbstractModule
    {
        private readonly Graph _affiliationGraph = new Graph();
        private readonly AffiliationFactory _affiliationFactory = new AffiliationFactory();
        private Dictionary<AffiliationEnum, Affiliation> _affiliationMap = new Dictionary<AffiliationEnum, Affiliation>();

        public override void initialize()
        {
            foreach (AffiliationEnum affiliationEnum in Enum.GetValues(typeof(AffiliationEnum)))
            {
                Affiliation affiliation = _affiliationFactory.createAffiliation(affiliationEnum);
                _affiliationGraph.addNode(affiliation);
                _affiliationMap.Add(affiliationEnum,affiliation);
            }
            _addEdge(AffiliationEnum.BLUE, AffiliationEnum.RED, AffiliationEdgeEnum.ATTACK);
            
            foreach (KeyValuePair<AffiliationEnum,Affiliation> keyValuePair in _affiliationMap)
            {
                keyValuePair.Value.initialize();
            }
        }
        
        private void _addEdge(AffiliationEnum source, AffiliationEnum target, AffiliationEdgeEnum edge)
        {
            _affiliationGraph.addBidirectionalEdge(getAffiliation(source), getAffiliation(target), _affiliationFactory.createAffiliationEdge(getAffiliation(source), getAffiliation(target), edge));
        }
        
        public override void start()
        {
            foreach (KeyValuePair<AffiliationEnum,Affiliation> keyValuePair in _affiliationMap)
            {
                keyValuePair.Value.start();
            }
        }
        
        public Affiliation getAffiliation(AffiliationEnum affiliationEnum)
        {
            if (_affiliationMap.TryGetValue(affiliationEnum, out var affiliation))
            {
                return affiliation;
            }

            return null;
        }
        
        public bool canAttack(Affiliation source, Affiliation target)
        {
            return _affiliationGraph.haveEdge(source, target, AffiliationEdgeEnum.ATTACK);
        }

        public override void destroy()
        {
            foreach (KeyValuePair<AffiliationEnum,Affiliation> keyValuePair in _affiliationMap)
            {
                keyValuePair.Value.destroy();
            }
        }
    }
}