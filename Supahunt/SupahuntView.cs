using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{
    public class SupahuntView
    {
        public enum ActionMenu
        {
            NoSelection,
            Hunt,
            Rest,
            ShowMap,
            Move,
            ShowStats,
            Exit        
        }

        public void ShowHeader()
        {
            Console.WriteLine("################### SUPAHUNT====================>");
            Console.WriteLine("------------------------------------------------");
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void PageBreak()
        {
            Console.Write("------------------------------");
        }

        /// <summary>
        /// populate menu for players to chose option
        /// </summary>
        /// <returns></returns>
        public ActionMenu GetInput()
        {
            NewLine();
            Console.WriteLine("1- Hunt");
            Console.WriteLine("2- Rest");
            Console.WriteLine("3- Show Map");
            Console.WriteLine("4- Move");
            Console.WriteLine("5- Show Stats");
                     
            Console.Write("Please select option[1-5]:");           

            int option=0;
            bool inputResult = int.TryParse(Console.ReadLine(), out option);
            NewLine();
            return (ActionMenu)option;
        }

      

    }
}
