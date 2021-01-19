using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{

    /// <summary>
    /// Class to represent player and its actions
    /// </summary>
    public class Player
    {

        public delegate void ChoopaHandler();

        public event ChoopaHandler GetChoopa;

        public static List<string> defaultNames = new List<string>() { "Dutch", "Dylan" };
        List<HuntingLog> _huntingLogs = new List<HuntingLog>();

        public List<HuntingLog> HuntingLogs { get => _huntingLogs; }


        public string Name { get; set; }
        public string PlayerId { get; set; }
        public int Stamina { get; set; } = 3;
        public int NoofBores { get; set; }

        HuntingPoint _huntingPoint = null;

        public HuntingPoint CurrentHuntingPoint { get => _huntingPoint; }


        /// <summary>
        /// Called when player want to rest
        /// </summary>
        public void Rest()
        {

           
            // if stamina is 3 then return as stamina cannot be more then 3
            if (Stamina == 3)
            {
                Console.WriteLine("you have enough stamina do not require rest...");
                return;
            }

            // add stamina to 2 when player want to rest
            Stamina += 2;

            if (Stamina > 3)
                Stamina=3; 
        }

        /// <summary>
        /// Implementation of hunt action for a player
        /// </summary>
        /// <returns></returns>
        public bool Hunt()
        {
            // check stamina and if hunt was sucess at hunting point                     

            if(hasStamina() && _huntingPoint.IsHuntSuccess())
            {
                if (_huntingPoint.IsLast && _huntingPoint.BoresLeft == 0)
                {
                    GetChoopa();
                }
                // decrease stamina by 1 
                Stamina -= 1;
                // add bore to hunt count
                NoofBores++;
                return true;
            }          

            return false;
        }

        private bool hasStamina()
        {
            if (Stamina < 1)
            {
                Console.WriteLine("you have do not have enough stamina");
                return false;
            }

            return true;
        }

        /// <summary>
        /// set hunting point for a player
        /// </summary>
        /// <param name="huntingPoint"></param>
        public void SetHuntingPoint(HuntingPoint huntingPoint)
        {
            //decrease 1 stamina

            if (hasStamina())
            {
                if (_huntingPoint != null)
                    Stamina -= 1;

                _huntingPoint = huntingPoint;
                // add new hunting point to log
                _huntingLogs.Add(new HuntingLog { NodeName = CurrentHuntingPoint.Vertex, HuntingCount = 1 });

                Console.Write($"=> {Name} move to {_huntingPoint.Vertex} *");
            }
        }

       
        /// <summary>
        /// show current location of player
        /// </summary>
        public void ShowCurrentLocation()
        {
            Console.Write($"{Name}  Current Location *");
            CurrentHuntingPoint.DisplayNodeInfo();
        }

        /// <summary>
        /// show stats of player
        /// </summary>
        public void ShowStats()
        {           
            Console.WriteLine($"Name    : {Name}");
            Console.WriteLine($"Stamina : {Stamina}");
            Console.WriteLine($"Bores Hunted : {NoofBores}");
            Console.WriteLine($"Path Travelled : {string.Join(" ",HuntingLogs.Select(p=>p.NodeName).ToArray())} ");
        }
       

    }
}
