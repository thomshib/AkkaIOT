using System;
using Xunit;
using Akka.Actor;
using Akka.TestKit.Xunit2;
using TemperatureMonitor.Actors;
using TemperatureMonitor.Messages;

namespace TemperatureMonitorTests
{
    public class TemperatureMonitorShould:TestKit
    {
        [Fact]
        public void InitializeSensorMetaData()
        {
            var probe = CreateTestProbe();
            var sensor = Sys.ActorOf(TemperatureMonitorActor.Props("a","1"));
            sensor.Tell(new RequestMetadata(1), probe.Ref);

            var received = probe.ExpectMsg<RespondMetada>();
            Assert.Equal(1, received.RequestId);
            Assert.Equal("a", received.FloorId);
            Assert.Equal("a", received.SensorId);

        }
    }
}
