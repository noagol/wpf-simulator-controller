using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ServerModel : BaseNotify
    {
        private bool shouldStop;
        private float lon;
        private float lat;


        /// <summary>Initializes a new instance of the <see cref="ServerModel"/> class.</summary>
        public ServerModel()
        {
            // Initialize members
            shouldStop = false;
            lon = 0.0f;
            lat = 0.0f;

            // Run server thread
            Thread thread = new Thread(new ThreadStart(startServer));
            thread.Start();
        }


        /// <summary>
        ///   <para>
        ///  Gets or sets the longitude.
        /// </para>
        /// </summary>
        /// <value>The lon.</value>
        public float Lon
        {
            get { return lon; }
            set
            {
                lon = value;
            }
        }


        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The lat.</value>
        public float Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }


        /// <summary>Reads the until new line.</summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static string readUntilNewLine(BinaryReader reader)
        {
            char[] buffer = new char[1024];
            int i = 0;
            char last = '\0';

            // Read until new line character
            while (i < 1024 && last != '\n')
            {
                char input = reader.ReadChar();
                buffer[i] = input;
                last = buffer[i];
                i++;
            }

            return new string(buffer);
        }


        /// <summary>Starts the server.</summary>
        private void startServer()
        {
            // Start server
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                    Properties.Settings.Default.FlightInfoPort);
            TcpListener server = new TcpListener(ep);
            server.Start();

            // Wait for client
            TcpClient clientSocket = server.AcceptTcpClient();

            // Get stream to client
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);

            // Start time for warmup
            DateTime start = DateTime.UtcNow;

            // Input buffer
            string inputLine;
            string[] splitted;

            // Warm up time
            // Ignore the first sent arguments
            while (!shouldStop && Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
            {
                inputLine = readUntilNewLine(reader);
                continue;
            }

            // Until stop is called
            while (!shouldStop)
            {
                // Read new line
                inputLine = readUntilNewLine(reader);

                // Split by comma
                splitted = inputLine.Split(',');

                // Extract values
                Lon = float.Parse(splitted[0]);
                Lat = float.Parse(splitted[1]);
            }

            // Close sockets
            clientSocket.Close();
            server.Stop();
        }
    }
}
