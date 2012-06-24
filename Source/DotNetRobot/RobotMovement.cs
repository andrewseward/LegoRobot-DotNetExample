using System;
using System.Collections.Specialized;
using NKH.MindSqualls;

namespace DotNetRobot
{
    public class RobotMovement : IRobotMovement
    {
        private readonly NxtBrick _nxtBrick;

        private readonly NxtMotorSync _motorPair;
        private readonly sbyte _speed;


        public RobotMovement(NxtBrick nxtBrick, NameValueCollection config)
        {
            _speed = sbyte.Parse(config["Speed"]);
            _nxtBrick = nxtBrick;
            var motorB = new NxtMotor();
            var motorC = new NxtMotor();
            _nxtBrick.MotorB = motorB;
            _nxtBrick.MotorC = motorC;
            _motorPair = new NxtMotorSync(motorB, motorC);
        }

        public void MoveForward()
        {
            Console.WriteLine("Moving forwards");
            _motorPair.Brake();
            _motorPair.Run(_speed, 0, 0);
        }

        public void Stop()
        {
            Console.WriteLine("Stopping");
            _motorPair.Brake();
        }

        public void Turn(bool clockwise)
        {
            Console.WriteLine("Turning");
            _motorPair.Brake();
            _motorPair.Run(_speed, 0, 100);
        }
    }

    public interface IRobotMovement
    {
        void MoveForward();
        void Stop();
        void Turn(bool clockwise);
    }
}