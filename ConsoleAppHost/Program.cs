using System;
using Akka.Actor;
using TemperatureMonitor.Messages;
using TemperatureMonitor.Actors;
using System.Threading;

using System.Threading.Tasks;

namespace ConsoleAppHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using(var system = ActorSystem.Create("IOT-System")){
                IActorRef floorManager = system.ActorOf(Props.Create<FloorManager>(),"floors-manager");
                await CreateSimulatedSensors(floorManager);

                while(true){
                    Console.WriteLine("Press Q to quit");
                    var cmd = Console.ReadLine();
                    if(cmd.ToUpperInvariant() =="Q"){
                        Environment.Exit(0);
                    }

                }

            }
        }

        private static async Task CreateSimulatedSensors(IActorRef floorManager){

        for(int sensorId = 0; sensorId < 10; sensorId++){
            var newSimulatorSensor = new SimulatedSensor("basement", $"{sensorId}", floorManager);
            
            await newSimulatorSensor.Connect();

            newSimulatorSensor.StartSendingSimulatedReadings();

        }
    }


    }


   
}
