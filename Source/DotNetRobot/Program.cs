using System;
using System.Configuration;
using NKH.MindSqualls;

namespace DotNetRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.AppSettings;
            var comPort = byte.Parse(config["ComPort"]);
            
            var nxtBrick = new NxtBrick(NxtCommLinkType.Bluetooth, comPort);
            
            var robotMovement = new RobotMovement(nxtBrick, config);
            var robotSenses = new RobotSenses(nxtBrick, config);
            var robotBrain = new RobotBrain(nxtBrick, robotMovement, robotSenses);
            Console.WriteLine("Starting Robot...");
            robotBrain.Start();
            Console.WriteLine("Robot started, press any key to stop");
            Console.ReadKey();
            robotBrain.Stop();
            
        }
    }
}
