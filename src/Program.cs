using System;

namespace dotnet_core_reaction_speed_game
{
    class Program
    {
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

            var game = new ReactionSpeedGame(simulate,mqttInterface);

            game.StartGame();
        }
    }
}
