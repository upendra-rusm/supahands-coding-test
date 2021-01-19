using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{
    public abstract class GraphBase
    {

        protected abstract IEnumerable<NodeBase> _nodes { get; set; }

        // list of nodes in graph (vetices)
        protected int NumVertices
        {
            get
            {
                if (_nodes == null)
                    return 0;

                return this._nodes.Count();
            }
        }


        public GraphBase()
        {
            
        }
       

        public GraphBase(IEnumerable<NodeBase> nodeList)
        {
            _nodes = nodeList;
        }


        /// <summary>
        /// Search and return node by vertex name 
        /// </summary>
        /// <param name="v">vertex name</param>
        /// <returns></returns>
        protected NodeBase GetNode(char v)
        {
            var result = _nodes.FirstOrDefault(n => n.Vertex == v);
            return result!=null? result:null;            
        } 
           


        /// <summary>
        /// Print funtion to print all path in graph 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        public void PrintAllPaths(char src, char des)
        {
            bool[] isVisited = new bool[_nodes.Count()];
            List<char> pathList = new List<char>();

            // add source to path[] 
            pathList.Add(src);
            Console.WriteLine("Map-Full Path List");
            Console.WriteLine("-------------------------------------------");
            // Call recursive utility 
            traversePath(src, des, isVisited, pathList);
            Console.WriteLine("==========================================");
        }

        /// <summary>
        /// Implementation to traverse path of graph  and print
        /// </summary>
        /// <param name="u"></param>
        /// <param name="des"></param>
        /// <param name="isVisited"></param>
        /// <param name="localPathList"></param>
        private void traversePath(char src, char des,bool[] isVisited,List<char> localPathList)
        {

            if (src.Equals(des))
            {
                Console.Write(string.Join(" ", localPathList));
                Console.Write(" || ");
                // if match found then no need to traverse more till depth 
                return;
            }

            int ind = GetIndexOf(src);
            // Mark the current node 
            isVisited[ind] = true;

            // Recur for all the vertices adjacent to current vertex 
            foreach (char edge in GetNode(src).Edges)
            {
                int ind2 = GetIndexOf(edge);
                if (!isVisited[ind2])
                {
                    // store current node in path[] 
                    localPathList.Add(edge);
                    traversePath(edge, des, isVisited,localPathList);

                    // remove current node in path[] 
                    localPathList.Remove(edge);
                }
            }

            // Mark the current node 
            int cInd = GetIndexOf(src);
            isVisited[cInd] = false;
        }

        /// <summary>
        /// return index to node from a graph
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetIndexOf(char item)
        {
            for (int i = 0; i < _nodes.Count(); i++)
            {
                var node = _nodes.ElementAt(i);
                if (node.Vertex == item)
                {
                    
                    return i;
                }

            }
            return 0;
        }       

       

    }
}
