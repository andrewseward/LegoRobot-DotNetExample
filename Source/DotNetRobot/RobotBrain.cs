using System;
using NKH.MindSqualls;

namespace DotNetRobot
{
    public interface IRobotBrain
    {
        void Start();
        void Stop();
    }

    public class RobotBrain : IRobotBrain
    {
        private readonly NxtBrick _nxtBrick;
        private readonly IRobotMovement _robotMovement;
        private readonly IRobotSenses _robotSenses;


        public RobotBrain(NxtBrick nxtBrick, IRobotMovement robotMovement, IRobotSenses robotSenses)
        {
            _nxtBrick = nxtBrick;
            _robotMovement = robotMovement;
            _robotSenses = robotSenses;
        }

        public void Start()
        {
            _robotSenses.OnObstacleEncountered += _robotSenses_OnObstacleEncountered;
            _robotSenses.OnObstacleCleared += new EventHandler(_robotSenses_OnObstacleCleared);
            _nxtBrick.Connect();
            _robotMovement.MoveForward();
        }

        void _robotSenses_OnObstacleCleared(object sender, EventArgs e)
        {
            _robotMovement.MoveForward();
        }

        void _robotSenses_OnObstacleEncountered(object sender, EventArgs e)
        {
            _robotMovement.Turn(true);
        }

        public void Stop()
        {
            _robotMovement.Stop();
            _nxtBrick.Disconnect();
        }
    }
}