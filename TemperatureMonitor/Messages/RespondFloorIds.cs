

using System.Collections.Generic;
using System.Collections.Immutable;

namespace TemperatureMonitor.Messages{


    public sealed class RespondFloorIds{
        public long RequestId{get;}
        public IImmutableSet<string> Ids{get;}
        public RespondFloorIds(long requestId,IImmutableSet<string> ids)
        {
            this.RequestId = requestId;
            Ids = ids;
        }
        
    }
}