using System;
using System.Threading;
using System.Device.Gpio;
using dotnet_core_reaction_speed_game.enums;
using dotnet_core_reaction_speed_game.models;

namespace dotnet_core_reaction_speed_game
{
    public class ReactionSpeedGame{

        private GpioController controller;

        private IDeviceService device;

        private GameConfig config;
        

        private GameData session;

        public ReactionSpeedGame(bool UseGpio, bool UseMqttInterface)
        {
            config = new GameConfig();
            config.UseGpio = !UseGpio;
        }

        public void StartGame(){
            Console.WriteLine("*********************************************");
            Console.WriteLine("* Game is initializing...                   *");
            Console.WriteLine("*********************************************");

            // Check if Gpio should be used?
            if(config.UseGpio) device = new GpioSerivce();
            else {
                Console.WriteLine("Will simulate Gpio, you need to use keyboard keys 1-5!");
                device = new KeyboardService();
            }
            
            // Init check that leds are working
            TurnOnLedsWithDelayAndTurnOff();
            
            GameLoop();
        }

        private void GameLoop(){
            Console.Clear();
            Console.WriteLine("*********************************************");
            Console.WriteLine("* Game is started.                          *");
            Console.WriteLine("*********************************************");

            while(config.RunGame){
                
                // Reset game data
                session = new GameData();

                Console.WriteLine("Press the illuminated button to start a new game!");
                device.SetLedState(LedType.LED3, true);
                
                // Wait until the middle switch has been pressed.
                config.RunGame = device.WaitForButtonPress(2);
              

                // Loop through all the leds and turn them on.
                device.SetAllLedStates(true);
                ShowGameStats();

                // Start our countdown
                Console.WriteLine("Starting in 5!");
                Thread.Sleep(1000); // Wait 1 second

                device.SetLedState(LedType.LED1, false);
                ShowGameStats();
                Console.WriteLine("4!");
                Thread.Sleep(1000); // Wait 1 second

                device.SetLedState(LedType.LED2, false);
                ShowGameStats();
                Console.WriteLine("3!");
                Thread.Sleep(1000); // Wait 1 second

                device.SetLedState(LedType.LED3, false);
                ShowGameStats();
                Console.WriteLine("2!");
                Thread.Sleep(1000); // Wait 1 second

                device.SetLedState(LedType.LED4, false);
                ShowGameStats();
                Console.WriteLine("1!");
                Thread.Sleep(1000); // Wait 1 second

                device.SetLedState(LedType.LED5, false);
                ShowGameStats();
                Console.WriteLine("Go Go Go!");
                Thread.Sleep(1000); // Wait 1 second

                // Start session
                while(session.Presses < config.NrOfLoop){
                    
                    session.Presses += 1; // Increment our counter variable by 1.

                    var random = new Random();

                    var random_delay =  random.Next(500,1500) / 1000; //Create a random number to be used as a delay to turn on a led. 
		            var random_number = random.Next(0,device.Leds.Length); //Create another random number to be used to turn on one of the leds.

                    Thread.Sleep(random_delay); // Wait for a random amount of time, as defined above

                    device.SetLedState(device.Leds[random_number], true); // Turn on a random led, as defined above
                    ShowGameStats();

                    var start = DateTime.Now; // Take a note of the time when the led was illuminated (so we can see how long it takes for the player to press the button)

                    device.WaitForButtonPressInGame(random_number);

                    var end = DateTime.Now; // Take note of the time when the button was pressed.
                    var timeTaken = end - start; // Calculate the time it took to press the button.
                    session.TotalTime += timeTaken; // Add time to total time.
                    device.SetLedState(device.Leds[random_number], false);
                    ShowGameStats();
                    
                    Console.WriteLine("Time taken: {0}", timeTaken);

                    if( device.CorrectButtonPressed){
                        var points = Math.Round(20.0 - ((timeTaken.TotalSeconds*10.0)-1.0),2); // Crude points system. Score between 0 - 10 points. If you take longer than 1 second you score 0. If you take less than 0.1 seconds you score 10.
                        
                        Console.WriteLine("{0} points added to your score!", points);
                        if (points < 0) // This just makes sure you don't get a negative point
                            points = 0;
                        
                        session.TotalScore += (int)points; // Add your points to your total score

                    }else{ // If you press the wrong button (not the button illuminated) you will lose some points!! 
                        Console.Write("{0} points deducted from your score!", config.Deduction);
                        session.NrOfWrongPresses += 1;
                        session.TotalScore -= config.Deduction;
                    }
                    Console.WriteLine("New score: {0}", session.TotalScore);
                    Console.WriteLine("Total time: {0}", session.TotalTime);
                        
                    Thread.Sleep(1);
                }

                // Once the game is over do a little flashy sequence.
                for (var x=0; x<5; x++){
                    for(var y=0; y<device.Leds.Length; y++){ 
                        device.SetLedState(device.Leds[y], true);
                        ShowGameStats();
                        Thread.Sleep(200);
                        device.SetLedState(device.Leds[y], false);
                        ShowGameStats();
                    }
                }

                
                Console.WriteLine("New score: {0}", session.TotalScore);
                Console.WriteLine("Total time: {0}", session.TotalTime);
                Console.WriteLine("Wrong presses: {0}", session.NrOfWrongPresses);
                
            }
        }

        private void TurnOnLedsWithDelayAndTurnOff(){
            // Start light 1
            Console.WriteLine("- Starting led 1");
            device.SetLedState(LedType.LED1, true);
            Thread.Sleep(1000);

            // Start light 2
            Console.WriteLine("- Starting led 2");
            device.SetLedState(LedType.LED2, true);
            Thread.Sleep(1000);

            // Start light 3
            Console.WriteLine("- Starting led 3");
            device.SetLedState(LedType.LED3, true);
            Thread.Sleep(1000);

            // Start light 4
            Console.WriteLine("- Starting led 4");
            device.SetLedState(LedType.LED4, true);
            Thread.Sleep(1000);

            // Start light 5
            Console.WriteLine("- Starting led 5");
            device.SetLedState(LedType.LED5, true);
            Thread.Sleep(1000);

            device.SetAllLedStates(false);
        }
      
       private void ShowGameStats(){
            Console.WriteLine("[{0}]         [{1}]", device.LedStatus[0] ? "*":"-", device.LedStatus[1] ? "*":"-");
            Console.WriteLine("      [{0}]", device.LedStatus[2] ? "*":"-");
            Console.WriteLine("[{0}]         [{1}]", device.LedStatus[3] ? "*":"-", device.LedStatus[4] ? "*":"-");
        }
    }
}