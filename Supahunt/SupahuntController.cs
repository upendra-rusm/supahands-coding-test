using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supahunt
{
    /// <summary>
    /// This class will control all actions 
    /// </summary>
    public class SupahuntController
    {

        private static SupahuntController controller = null;

        private HuntingMap _huntingMap;

        public SupahuntView View=new SupahuntView();
        public bool exitGame = false;

        List<Player> players = null;
        List<Player> Players { get => players; }

        private int playerIndex = -1;

        Player player = null;


        private SupahuntController(HuntingMap huntingMap)
        {
            players = new List<Player>();
            _huntingMap = huntingMap;
        }

        public static SupahuntController Instance()
        {
            if (controller == null)
            {                
                controller = new SupahuntController(new HuntingMap());                 
            }
            return controller;        
        }    


        /// <summary>
        /// Return nexts player from the players list
        /// </summary>
        /// <returns></returns>
        public void NextPlayer()
        {
            
            if (playerIndex == Players.Count - 1)
            {
                playerIndex = 0;
            }
            else
            ++playerIndex;
            
            SupahuntView.PageBreak();
            SupahuntView.NewLine();
            Console.WriteLine($"{Players[playerIndex].Name} ! your turn .. ");
            SupahuntView.PageBreak();
            player =Players[playerIndex];

            if (player.IsDone)
                NextPlayer();
        }


        /// <summary>
        /// Adds new player to player list
        /// </summary>
        public void AddPlayer(int noOfPlayers)
        {
            Console.WriteLine("Add Players !");

            for (int i = 1; i <= noOfPlayers; i++)
            {
                Console.Write($"Add Player {Players.Count + 1} [Press y for Default {Player.defaultNames[Players.Count]}]:");
                string playerName = Console.ReadLine();
                Players.Add(new Player { Name = playerName == "y" || playerName == "" ? Player.defaultNames[Players.Count] : playerName });
                Console.WriteLine($"Great! Player {Players.Count} {Players[Players.Count - 1].Name} added ! "); 
            }
        }

        /// <summary>
        /// set starting point and select first player
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Lets start !");

            foreach (var p in Players)
            {
                p.SetHuntingPoint(_huntingMap.GetStartPoint());
                SupahuntView.NewLine();
            }
            NextPlayer();
        }

        /// <summary>
        /// All game actions triggered by menu selection 
        /// </summary>
        /// <param name="action">action to be executed</param>
        public void ExecuteAction(Supahunt.SupahuntView.ActionMenu action)
        {
            if (CheckforChoopa())
            {
                action = Supahunt.SupahuntView.ActionMenu.Exit;
            }

            switch (action)
            {
                case SupahuntView.ActionMenu.Hunt:
                    ActionHunt();
                    break;
                case SupahuntView.ActionMenu.Rest:
                   ActionRest();
                    break;
                case SupahuntView.ActionMenu.ShowMap:
                    ActionShowMap();
                    break;
                case SupahuntView.ActionMenu.Move:
                    ActionMove();
                    break;
                case SupahuntView.ActionMenu.ShowStats:
                    player.ShowStats();
                    break;
                case SupahuntView.ActionMenu.Exit:                    
                    SupahuntView.NewLine();
                    foreach (var p in players)
                    {
                        p.ShowStats();
                    }
                    Console.WriteLine("It was fun . see you next time ........");
                    exitGame = true;
                    break;
                default:
                    Console.WriteLine("!!!!!! Opps ! Wrong Selection. Please choose agian. !!!!!!");
                    break;

            }
        }

        private bool CheckforChoopa()
        {

            foreach (var p in players)
            {
                if (p.IsDone == false)
                    return false;
            }
            Console.WriteLine("You got choopa");
            return true;
        }

        #region Actions

        /// <summary>
        /// Implementaion of show map information
        /// </summary>
        private void ActionShowMap()
        {
            // show all possible path to reach hunt point 
            _huntingMap.PrintAllPaths('A', 'K');
            SupahuntView.PageBreak();
            // show player current location and next hunting points 
            player.ShowCurrentLocation();
            SupahuntView.NewLine();
        }

        /// <summary>
        /// Move action from one hunting point to another
        /// </summary>
        private void ActionMove()
        {
            Console.Write("Enter node you want to move:");
            var moveTo = Console.ReadLine();
            char huntPoint = moveTo.ToUpper().ToCharArray()[0];
            if (_huntingMap.IsValidHuntingPoint(player.CurrentHuntingPoint, huntPoint))
            {
                player.SetHuntingPoint(_huntingMap.GetHuntingPoint(huntPoint));
                SupahuntView.PageBreak();
                SupahuntView.NewLine();
                NextPlayer();
            }
            else if (player.CurrentHuntingPoint.IsLast)
            {
                Console.WriteLine($"This is last point you cant move any more.");
            }
            else
            {
                Console.WriteLine($"Opps! {player.Name} wrong move .Try again or refer map");
            }

        }

        /// <summary>
        /// Rest action when player want to rest 
        /// </summary>
        private void ActionRest()
        {
            if (player.Stamina > 1)
            {
                Console.Write("you have enough stamina to hunt or move. Do you still want to rest ? [y for yes or n for no]:");
                var ans = Console.ReadLine();

                if (ans == "y")
                {
                    player.Rest();
                    NextPlayer();
                }
            }
            else
            {
                player.Rest();
                NextPlayer();
            }
        }

        /// <summary>
        /// Hunt action when player want to hunt bore 
        /// </summary>
        private void ActionHunt()
        {
            if (player.Hunt())
            {
                NextPlayer();
            }
        }

        #endregion

    }
}
