using System;
using Akka.Actor;
using TemperatureMonitor.Messages;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace TemperatureMonitor.Actors
{
    public class FloorManager : UntypedActor
    {
       
        private Dictionary<string,IActorRef> _floorsList = new Dictionary<string, IActorRef>();


        public FloorManager()
        {
     
           
        }

        protected override void OnReceive(object message)
        {
            switch(message){
               
                case RequestRegisterSensor msg:


                    if(_floorsList.TryGetValue(msg.FloorId, out var existingFloorActorRef)){
                        existingFloorActorRef.Forward(msg);

                    } else{

                        var newFloorActor = Context.ActorOf(Floor.CreateFloorActor(msg.FloorId),
                        $"floor-{msg.FloorId}");

                        Context.Watch(newFloorActor);

                        _floorsList.Add(msg.FloorId, newFloorActor);

                        newFloorActor.Forward(msg);
                    }
                                  
                    break;

                case RequestFloorIds msg:
                     Sender.Tell(new RespondFloorIds(msg.RequestId, ImmutableHashSet.CreateRange(_floorsList.Keys)));
                    break;


               

                case Terminated msg:
                    var terminatedFloorId = _floorsList.First(x =>  x.Value == msg.ActorRef).Key;
                    _floorsList.Remove(terminatedFloorId);
                    break;

                default:
                    Unhandled(message);
                    break;

            }
      
        }


        public static Props CreateFloorMangerActor() =>
            Props.Create<FloorManager>();
        




    }
}