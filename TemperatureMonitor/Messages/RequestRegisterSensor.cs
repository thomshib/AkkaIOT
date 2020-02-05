
namespace TemperatureMonitor.Messages{


    public sealed class RequestRegisterSensor{
        public long RequestId{get;}
        public string FloorId{get;}
        public string SensorId{get;}
        public RequestRegisterSensor(long requestId,string floorId,string sensorId)
        {
            this.RequestId = requestId;
            this.FloorId = floorId;
            this.SensorId = sensorId;
        }
        
    }
}