

using System.Collections.Generic;
using System.Collections.Immutable;

namespace TemperatureMonitor.Messages{


    public sealed class RespondTemperatureSensorIds{
        public long RequestId{get;}
        public IImmutableSet<string> Ids{get;}
        public RespondTemperatureSensorIds(long requestId,IImmutableSet<string> ids)
        {
            this.RequestId = requestId;
            Ids = ids;
        }
        
    }
}