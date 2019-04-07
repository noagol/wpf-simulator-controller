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


        /// <summary>Initializes a new instance of the <see cref="ConnectModel"/> class.</summary>
        public ConnectModel()
        {
            // Set stop boolean
            shouldStop = false;

            // Initialize commands queue
            commandsQueue = new Queue<SetCommand>();

            // Intialize members
            throttle = 0.0f;
            ip = Properties.Settings.Default.FlightServerIP;
            port = Properties.Settings.Default.FlightCommandPort;

            // Start sender thread
            thread = new Thread(new ThreadStart(updateSimulator));
            thread.Start();
        }


        /// <summary>
        ///   <para>
        /// Gets the ip.
        /// </para>
        /// </summary>
        /// <value>The ip.</value>
        public string IP
        {
            get { return ip; }
        }

        /// <summary>Gets the port.</summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return port; }
        }


        /// <summary>Gets or sets the throttle.</summary>
        /// <value>The throttle.</value>
        public float Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                update("/controls/engines/current-engine/throttle", value);
            }
        }


        /// <summary>
        ///   <para>
        ///  Gets or sets the rudder.
        /// </para>
        /// </summary>
        /// <value>The rudder.</value>
        public float Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                update("/controls/flight/rudder", value);
            }
        }


        /// <summary>Gets or sets the aileron.</summary>
        /// <value>The aileron.</value>
        public float Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                update("/controls/flight/aileron", value);
            }
        }


        /// <summary>Gets or sets the elevator.</summary>
        /// <value>The elevator.</value>
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                update("/controls/flight/elevator", value);
            }
        }


        /// <summary>Stops this instance.</summary>
        public void stop()
        {
            shouldStop = true;
            thread.Join();
        }


        /// <summary>Add set command to queue</summary>
        /// <param name="path">The path.</param>
        /// <param name="value">The value.</param>
        private void update(string path, float value)
        {
            // Create command
            string command = String.Format("set {0} {1}", path, value);
            // Create command object
            SetCommand setCommand = new SetCommand(command, false);
            // Add to queue
            addCommand(setCommand);
        }


        /// <summary>Adds the command.</summary>
        /// <param name="command">The command.</param>
        public void addCommand(SetCommand command)
        {
            command.Command += "\r\n";
            commandsQueue.Enqueue(command);
        }


        /// <summary>Updates the simulator.</summary>
        private void updateSimulator()
        {
            // Connect to client
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), Port);
            TcpClient client = new TcpClient();
            client.Connect(ep);

            // Run until stop is called
            while (!shouldStop)
            {
                // Wait for commands to enter queue
                if (commandsQueue.Count != 0)
                {
                    using (NetworkStream stream = new NetworkStream(client.Client, false))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        while (commandsQueue.Count != 0)
                        {
                            // Dequeue command
                            SetCommand command = commandsQueue.Dequeue();
                            // Change decoding to ASCII
                            byte[] data = System.Text.Encoding.ASCII.GetBytes(command.Command);

                            // Write to socket
                            writer.Write(data);
                            writer.Flush();

                            // Delay command execution if needed
                            if (command.ShouldDelay)
                            {
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }

            // Close client socket
            client.Close();
        }
    }
}
