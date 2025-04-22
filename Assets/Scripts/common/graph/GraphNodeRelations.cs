using System;
using System.Collections.Generic;
using affiliation;

namespace common
{
    public class GraphNodeRelations
    {
        readonly Dictionary<Enum,GraphEdge> _edgeMap;
        
        public GraphNodeRelations()
        {
            _edgeMap = new Dictionary<Enum, GraphEdge>();
        }
        
        public void addEdge(GraphEdge edge)
        {
            _edgeMap.Add(edge.edgeType, edge);
        }
        
        public void removeEdge(Enum edgeType)
        {
            _edgeMap.Remove(edgeType);
        }
        
        public bool haveEdge(Enum edgeType)
        {
            return _edgeMap.ContainsKey(edgeType);
        }
        
    }
}