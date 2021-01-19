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
        public static List<string> defaultNames = new List<string>() { "Dutch", "Dylan" };
        List<HuntingLog> _huntingLogs = new List<HuntingLog>();

        public List<HuntingLog> HuntingLogs { get => _huntingLogs; }


        public string Name { get; set; }
        public string PlayerId { get; set; }
        public int Stamina { get; set; } = 3;
        public int NoofBores { get; set; }

        public bool IsDone = false;

        public bool IsResting = false;

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
           
            IsResting = true;
            
        }

        /// <summary>
        /// Implementation of hunt action for a player
        /// </summary>
        /// <returns></returns>
        public bool Hunt()
        { 
            // check if hunt was sucess at hunting point 
            if(_huntingPoint.IsHuntSuccess())
            {
                // decrease stamina by 1 
                Stamina-= 1;
                // add bore to hunt count
                NoofBores++;
                return true;
            }

            if (_huntingPoint.IsLast && _huntingPoint.BoresLeft==0)
            {
                IsDone = true;
                return false;
            }

            return false;
        }

        /// <summary>
        /// set hunting point for a player
        /// </summary>
        /// <param name="huntingPoint"></param>
        public void SetHuntingPoint(HuntingPoint huntingPoint)
        {
            _huntingPoint = huntingPoint;
            // add new hunting point to log
            _huntingLogs.Add(new HuntingLog { NodeName = CurrentHuntingPoint.Vertex, HuntingCount = 1 });
            //decrease 1 stamina
            Stamina -= 1;
            Console.Write($"=> {Name} move to {_huntingPoint.Vertex} *");
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
