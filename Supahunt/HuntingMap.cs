using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{

    /// <summary>
    /// this class stoed hunting location and provide Implementation for map related features
    /// </summary>
    public class HuntingMap:GraphBase
    {

        private List<HuntingPoint> _huntingPoints=null;
        protected override IEnumerable<NodeBase> _nodes { get => _huntingPoints; set => value = _huntingPoints; }

        public HuntingMap()
        {
            _huntingPoints = new List<HuntingPoint>();
            _huntingPoints.Add(new HuntingPoint('A', new char[] { 'B', 'C', 'K' }));
            _huntingPoints.Add(new HuntingPoint('B', new char[] { 'D', 'E' }));
            _huntingPoints.Add(new HuntingPoint('C', new char[] { 'E', 'G', 'H' }));
            _huntingPoints.Add(new HuntingPoint('D', new char[] { 'E', 'F' }));
            _huntingPoints.Add(new HuntingPoint('E', new char[] { 'G', 'I', 'F' }));
            _huntingPoints.Add(new HuntingPoint('F', new char[] { 'I', 'J' }));
            _huntingPoints.Add(new HuntingPoint('G', new char[] { 'I', 'K' }));
            _huntingPoints.Add(new HuntingPoint('H', new char[] { 'I', 'F' }));
            _huntingPoints.Add(new HuntingPoint('I', new char[] { 'K' }));
            _huntingPoints.Add(new HuntingPoint('J', new char[] { 'K' }));
            _huntingPoints.Add(new HuntingPoint('K', null));
            _huntingPoints.Last().IsLast = true;
        }

        public HuntingMap(List<HuntingPoint> huntingPoints)
        {
            _huntingPoints = huntingPoints;           
        }

        public HuntingPoint GetStartPoint()
        {
            return _huntingPoints.First();        
        }       

        /// <summary>
        /// Check if hunting point query is valid or not 
        /// </summary>
        /// <param name="current">current hunting point</param>
        /// <param name="newPoint">new huntingg point</param>
        /// <returns></returns>
        public bool IsValidHuntingPoint(HuntingPoint current,char newPoint)
        {
            if (current.Vertex == newPoint)
            {
                Console.WriteLine("This is same as your current hunting point .");
                return false;
            } 
            return current.ContainsEdge(newPoint);
        }

        /// <summary>
        /// return hunting point by name
        /// </summary>
        /// <param name="huntingPoint"></param>
        /// <returns></returns>
        public HuntingPoint GetHuntingPoint(char huntingPoint)
        {
            return ConvertTo(GetNode(huntingPoint));
        }       


        /// <summary>
        /// cast nodebase list to hinting point list
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        private List<HuntingPoint> ConvertTo(IEnumerable<NodeBase> nodeList)
        {
            var result = from n in nodeList select ConvertTo(n);
            return result.ToList();
        }

        /// <summary>
        /// cast nodebase to hunting point
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private HuntingPoint ConvertTo(NodeBase node)
        {
            return node as HuntingPoint;        
        }

    }
}
