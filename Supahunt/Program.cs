using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{
    class Program
    {
        static void Main(string[] args)
        {           
           
            SupahuntController supahuntController = SupahuntController.Instance();
            supahuntController.View.ShowHeader(); 
            
            // add number of players want to play
            supahuntController.AddPlayer(2); 
            // sett started point for all players
            supahuntController.Start();
            
            do
            {                
                supahuntController.ExecuteAction(supahuntController.View.GetInput());
                
            }
            while (supahuntController.exitGame == false);         

        }
    }
}
