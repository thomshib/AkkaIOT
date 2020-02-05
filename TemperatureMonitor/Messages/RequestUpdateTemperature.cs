
namespace TemperatureMonitor.Messages{


    public sealed class RequestUpdateTemperature{
        public long RequestId{get;}
        public double Temperature{get;}
        public RequestUpdateTemperature(long requestId,double temperature)
        {
            this.RequestId = requestId;
            this.Temperature = temperature;
        }
        
    }
}