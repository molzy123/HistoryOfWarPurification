using System;
using System.Collections.Generic;
using affiliation;

namespace common
{
    public class Graph
    {
        List<GraphNode> _nodes;
        private Dictionary<(GraphNode,GraphNode),GraphNodeRelations> _relationsMap;
        
        
        public Graph()
        {
            _nodes = new List<GraphNode>();
            _relationsMap = new Dictionary<(GraphNode, GraphNode), GraphNodeRelations>();
        }
        
        public void addNode(GraphNode node)
        {
            _nodes.Add(node);
        }
        
        public void removeNode(GraphNode node)
        {
            _nodes.Remove(node);
        }
        
        
        public void addEdge(GraphNode source, GraphNode target, GraphEdge edge)
        {
            if (!_nodes.Contains(source) || !_nodes.Contains(target))
            {
                throw new Exception("Source or target node not in graph");
            }
            acquireRelations(source, target).addEdge(edge);
        }
        
        // Add bidirectional edges
        public void addBidirectionalEdge(GraphNode source, GraphNode target, GraphEdge edge)
        {
            addEdge(source, target, edge);
            addEdge(target, source, edge);
        }
        
        public void removeEdge(GraphNode source, GraphNode target,Enum edgeType)
        {
            if (!_nodes.Contains(source) || !_nodes.Contains(target)) return;
            if (!_relationsMap.ContainsKey((source, target))) return;
            acquireRelations(source, target).removeEdge(edgeType);
        }
        
        public bool haveEdge(GraphNode source, GraphNode target, Enum edge)
        {
            if (!_nodes.Contains(source) || !_nodes.Contains(target)) return false;
            if (!_relationsMap.ContainsKey((source, target))) return false;
            return acquireRelations(source, target).haveEdge(edge);
        }
        
        
        private GraphNodeRelations acquireRelations(GraphNode source, GraphNode target)
        {
            GraphNodeRelations relations;
            if (!_relationsMap.ContainsKey((source, target)))
            {
                relations = new GraphNodeRelations();
                _relationsMap.Add((source, target), relations);
            }
            else
            {
                relations = _relationsMap[(source, target)];
            }

            return relations;
        }
        
    }
}