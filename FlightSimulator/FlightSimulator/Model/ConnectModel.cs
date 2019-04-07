using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulator.Model
{
    class ConnectModel
    {
        private float throttle;
        private float rudder;
        private float aileron;
        private float elevator;


        private static bool shouldStop;
        private static Queue<string> commandsQueue;

        public ConnectModel()
        {
            shouldStop = false;
            commandsQueue = new Queue<string>();
            throttle = 0.0f;

            Thread thread = new Thread(new ThreadStart(updateSimulator));
            thread.Start();
        }

        public float Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                update("/controls/engines/current-engine/throttle", value);
            }
        }

        public float Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                update("/controls/flight/rudder", value);
            }
        }

        public float Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                update("/controls/flight/aileron", value);
            }
        }

        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                update("/controls/flight/elevator", value);
            }
        }

        private void update(string path, float value)
        {
            string command = String.Format("set {0} {1}\r\n", path, value);
            commandsQueue.Enqueue(command);
        }

        private static void updateSimulator()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5402);
            TcpClient client = new TcpClient();
            client.Connect(ep);

            while (!shouldStop)
            {
                if (commandsQueue.Count != 0)
                {
                    using (NetworkStream stream = new NetworkStream(client.Client, false))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        while(commandsQueue.Count != 0)
                        {
                            string command = commandsQueue.Dequeue();
                            byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                            Console.WriteLine(command);
                            writer.Write(data);
                            writer.Flush();
                        }
                    }
                }
                Thread.Sleep(1000);
            }

            client.Close();

        }
    }
}
