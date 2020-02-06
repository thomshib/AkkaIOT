using Akka.Actor;
using TemperatureMonitor.Messages;
using TemperatureMonitor.Actors;
using System.Threading;
using System;
using System.Threading.Tasks;


namespace ConsoleAppHost
{
public class SimulatedSensor{
    private readonly string _floorId;
    private readonly string _sensorId;
    private IActorRef _floorManager;
    private IActorRef _sensorReference;
    private readonly Random _randomTemperatureGenerator;
    private Timer _timer;
    public SimulatedSensor(string floorId, string sensorId, IActorRef floorManager)
    {
        _floorId = floorId;
        _sensorId = sensorId;
        _floorManager = floorManager;
        _randomTemperatureGenerator = new Random(int.Parse(sensorId));
    }

    public async Task Connect(){
        var response = await _floorManager.Ask<RespondSensorRegistered>(
            new RequestRegisterSensor(1,_floorId,_sensorId)

        );

        _sensorReference = response.SensorReference;
    }

    public void StartSendingSimulatedReadings(){

        _timer = new Timer(SimulateUpdateTemperature,null,0,1000);
    }

    private void  SimulateUpdateTemperature(object sender){
        var randomTemperature = _randomTemperatureGenerator.NextDouble();
        randomTemperature *= 100;
        _sensorReference.Tell(new RequestUpdateTemperature(0,randomTemperature));
    }

}

}