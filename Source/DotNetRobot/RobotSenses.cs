using System;
using System.Collections.Specialized;
using NKH.MindSqualls;

namespace DotNetRobot
{
    public class RobotSenses : IRobotSenses
    {
        private readonly NxtBrick _nxtBrick;
        private readonly byte _thresholdDistanceCm;
        private bool _blocked;
        public event EventHandler OnObstacleEncountered;
        public event EventHandler OnObstacleCleared;


        public RobotSenses(NxtBrick nxtBrick, NameValueCollection config)
        {
            _nxtBrick = nxtBrick;
            _thresholdDistanceCm = byte.Parse(config["ThresholdDistanceCm"]);
            var distanceSensor = new NxtUltrasonicSensor
                                     {
                                         PollInterval = Int32.Parse(config["DistanceSensorPollingInterval"])
                                     };

            distanceSensor.OnPolled += distanceSensor_OnPolled;
            _nxtBrick.Sensor4 = distanceSensor;
        }

        void distanceSensor_OnPolled(NxtPollable polledItem)
        {
            var distanceSensor = (NxtUltrasonicSensor) polledItem;
            var distance = distanceSensor.DistanceCm;
            if (distance < _thresholdDistanceCm)
            {
                if (!_blocked)
                {
                    _blocked = true;
                    Console.WriteLine("Obstacle Encountered {0}", distance);
                    if (OnObstacleEncountered != null)
                    {
                        OnObstacleEncountered.Invoke(distanceSensor, new EventArgs());
                    }
                }
            }
            else
            {
                if (_blocked)
                {
                    _blocked = false;
                    Console.WriteLine("Obstacle Cleared {0}", distance);
                    if (OnObstacleCleared != null)
                    {
                        OnObstacleCleared.Invoke(distanceSensor, new EventArgs());
                    }
                }
            }
        }
    }

    public interface IRobotSenses
    {
        event EventHandler OnObstacleCleared;
        event EventHandler OnObstacleEncountered;
    }
}