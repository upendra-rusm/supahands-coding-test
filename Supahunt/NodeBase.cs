using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{

    /// <summary>
    /// this class represents node of a graph
    /// </summary>
    public abstract class NodeBase
    {

        private char _vertex;
        private IEnumerable<char> _edges;

        public char Vertex { get => _vertex; set => _vertex = value; }

        public List<char> Edges { get => _edges==null?null:_edges.ToList(); }
        

        public NodeBase()
        {
            _edges = new List<char>();
        }

        public NodeBase(char vertex,IEnumerable<char> edges)
        {
            _vertex = vertex;
            _edges = edges;
        }

        /// <summary>
        /// Checks if node contains edges
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool ContainsEdge(char edge)
        {
            return Edges.Contains(edge);        
        }

        /// <summary>
        /// Display details of node
        /// </summary>
        public void DisplayNodeInfo()
        {
            Console.Write($"{Vertex}:");
            if (Edges != null)
                Edges.ForEach(i => Console.Write($"{Vertex }->{i} "));           
        }
      
    }
}
