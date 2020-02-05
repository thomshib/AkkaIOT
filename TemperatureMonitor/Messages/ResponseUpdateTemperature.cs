
namespace TemperatureMonitor.Messages{


    public sealed class ResponseUpdateTemperature{
        public long RequestId{get;}
        public ResponseUpdateTemperature(long requestId)
        {
            this.RequestId = requestId;
  
        }
        
    }
}