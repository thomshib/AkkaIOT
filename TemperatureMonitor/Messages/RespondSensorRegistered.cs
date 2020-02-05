using Akka.Actor;
namespace TemperatureMonitor.Messages{


    public sealed class RespondSensorRegistered{
        public long RequestId{get;}
        public IActorRef SensorReference{get;}
       
        public RespondSensorRegistered(long requestId,IActorRef sensorReference)
        {
            this.RequestId = requestId;
            this.SensorReference = sensorReference;
           
        }
        
    }
}