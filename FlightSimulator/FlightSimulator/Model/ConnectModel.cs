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
        private string ip;
        private int port;
        private Thread thread;

        private bool shouldStop;
        private Queue<SetCommand> commandsQueue;

        public ConnectModel()
        {
            shouldStop = false;
            commandsQueue = new Queue<SetCommand>();
            throttle = 0.0f;
            ip = Properties.Settings.Default.FlightServerIP;
            port = Properties.Settings.Default.FlightCommandPort;

            thread = new Thread(new ThreadStart(updateSimulator));
            thread.Start();
        }

        public string IP
        {
            get { return ip; }
        }

        public int Port
        {
            get { return port; }
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

        public void stop()
        {
            shouldStop = true;
            thread.Join();
        }

        private void update(string path, float value)
        {
            string command = String.Format("set {0} {1}", path, value);
            SetCommand setCommand = new SetCommand(command, false);
            addCommand(setCommand);
        }
        
        public void addCommand(SetCommand command)
        {
            command.Command += "\r\n";
            commandsQueue.Enqueue(command);
        }

        private void updateSimulator()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), Port);
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
                            SetCommand command = commandsQueue.Dequeue();
                            byte[] data = System.Text.Encoding.ASCII.GetBytes(command.Command);
                            Console.WriteLine(command.Command);
                            writer.Write(data);
                            writer.Flush();
                            if (command.ShouldDelay)
                            {
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }

            client.Close();

        }
    }
}
