using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{
    public class HuntingPoint:NodeBase
    {
        readonly int NoOfBores = 3;
        
        public int _boresHunted { get; set; }        

        public int BoresLeft { get => NoOfBores - _boresHunted; }

        public bool IsLast = false;
       
        public HuntingPoint(char vertex, IEnumerable<char> edges) : base(vertex, edges)
        {

        }




        /// <summary>
        ///  checks if hunt is sucess at hunting point as each hunt point has 3 bores only 
        /// </summary>
        /// <returns></returns>
        public bool IsHuntSuccess()
        {
            if (NoOfBores == _boresHunted)
            {
                Console.WriteLine("Opps! all bores hunted . better move on.");
                return false;                
            }

            _boresHunted++;
            Console.WriteLine($"Yeah! you got one :-).{BoresLeft} bores left.  ");
            
            return true;
        }
    }
}
