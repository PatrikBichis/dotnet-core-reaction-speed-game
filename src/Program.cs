using System;

namespace dotnet_core_reaction_speed_game
{
    class Program
    {
        private static ReactionSpeedGame game;

        static void Main(string[] args)
        {
            var simulate = false;
            var mqttInterface = false;

            // Read args parameter to determine if Gpio should be simulated 
            // with keyboard and if we should post mqtt information.
            try{
                if(args.Length > 0){
                    foreach (var arg in args)
                    {
                        if(arg == "-s" || arg == "--simualte") simulate = true;
                        if(arg == "-m" || arg == "--mqtt") mqttInterface = true;
                        Console.WriteLine("arg: {0}",arg);
                    }
                }
            }catch(Exception ex){
                Console.WriteLine("Error when reading the args, will use defualt!!!");
            }

            // Add event to clean up on exit
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit); 

            game = new ReactionSpeedGame(simulate,mqttInterface);

            game.StartGame();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Starting to clean up...");
            game.CleanUp();
            Console.WriteLine("Clean up is done, closing application");
        }
    }
}
