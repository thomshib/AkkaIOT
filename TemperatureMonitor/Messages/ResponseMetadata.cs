
namespace TemperatureMonitor.Messages{


    public sealed class ResponseMetadata{
        public long RequestId{get;}
        public string FloorId{get;}
        public string SensorId{get;}
        public ResponseMetadata(long requestId,string floorId,string sensorId)
        {
            this.RequestId = requestId;
            this.FloorId = floorId;
            this.SensorId = sensorId;
        }
        
    }
}