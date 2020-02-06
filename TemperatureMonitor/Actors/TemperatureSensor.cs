using System;
using Akka.Actor;
using TemperatureMonitor.Messages;

namespace TemperatureMonitor.Actors
{
    public class TemperatureSensor : UntypedActor
    {
        private string _floorId;
        private string _sensorId;
        private double? _lastRecordedTemperature;

        public TemperatureSensor(string floorId, string sensorId)
        {
            _floorId = floorId;
            _sensorId = sensorId;
        }

        protected override void OnReceive(object message)
        {
            switch(message){
                case RequestMetadata msg:
                    //requestId extract for correlation of request and response
                    Sender.Tell(new ResponseMetadata(msg.RequestId,_floorId,_sensorId));
                    break;

                case RequestTemperature msg:
                    Sender.Tell(new RespondTemperature(msg.RequestId, _lastRecordedTemperature));
                    break;

                
                case RequestUpdateTemperature msg:
                    _lastRecordedTemperature = msg.Temperature;
                    Sender.Tell(new ResponseUpdateTemperature(msg.RequestId));
                    Console.WriteLine($"RequestUpdateTemperature message from Floor:{_floorId}::Sensor:{_sensorId}::Temperature:{_lastRecordedTemperature}");
                    break;

                case RequestRegisterSensor msg when
                    msg.FloorId == _floorId && msg.SensorId == _sensorId:
                    Console.WriteLine($"RequestRegister message from Floor:{_floorId}::Sensor:{_sensorId}");
                    Sender.Tell(new RespondSensorRegistered(msg.RequestId, Context.Self));                 
                    break;

                default:
                    Unhandled(message);
                    break;

            }
      
        }

        public static Props CreateTemperatureSensorActor(string floorId,string sensorId) =>
            Props.Create(() => new TemperatureSensor(floorId,sensorId));
        




    }
}