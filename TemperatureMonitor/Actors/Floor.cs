using System;
using Akka.Actor;
using TemperatureMonitor.Messages;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace TemperatureMonitor.Actors
{
    public class Floor : UntypedActor
    {
        private string _floorId;
        private Dictionary<string,IActorRef> _sensorsList = new Dictionary<string, IActorRef>();


        public Floor(string floorId)
        {
            _floorId = floorId;
           
        }

        protected override void OnReceive(object message)
        {
            switch(message){
               

                case RequestRegisterSensor msg when
                    msg.FloorId == _floorId :


                    if(_sensorsList.TryGetValue(msg.SensorId, out var existingSensorActorRef)){
                        existingSensorActorRef.Forward(msg);

                    } else{

                        var newTemperatureActor = Context.ActorOf(TemperatureSensor.CreateTemperatureSensorActor(_floorId,msg.SensorId),
                        $"temperature-sensor-{msg.SensorId}");

                        Context.Watch(newTemperatureActor);
                        _sensorsList.Add(msg.SensorId, newTemperatureActor);
                        newTemperatureActor.Forward(msg);
                    }

                                  
                    break;

                case RequestTemperatureSensorIds msg:
                        Sender.Tell(new RespondTemperatureSensorIds(msg.RequestId, ImmutableHashSet.CreateRange(_sensorsList.Keys)));
                        break;

                case Terminated msg:
                    var terminatedTemperatureId = _sensorsList.First(x =>  x.Value == msg.ActorRef).Key;
                    _sensorsList.Remove(terminatedTemperatureId);
                    
                    break;

                default:
                    Unhandled(message);
                    break;

            }
      
        }


        public static Props CreateFloorActor(string floorId) =>
            Props.Create(() => new Floor(floorId));
        




    }
}