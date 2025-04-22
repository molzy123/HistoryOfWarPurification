using System;

namespace common
{
    public class GraphEdge
    {
        public GraphNode source;
        public GraphNode target;

        public Enum edgeType;
        
        public GraphEdge(GraphNode source, GraphNode target)
        {
            this.source = source;
            this.target = target;
        }
        
    }
}