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

        public ServerModel()
        {
            shouldStop = false;
            lon = 0.0f;
            lat = 0.0f;

            Thread thread = new Thread(new ThreadStart(startServer));
            thread.Start();
        }

        public float Lon
        {
            get { return lon; }
            set
            {
                lon = value;
            }
        }

        public float Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        private static string readUntilNewLine(BinaryReader reader)
        {
            char[] buffer = new char[1024];
            int i = 0;
            char last = '\0';

            while (i < 1024 && last != '\n')
            {
                char input = reader.ReadChar();
                buffer[i] = input;
                last = buffer[i];
                i++;
            }

            return new string(buffer);
        }


        private void startServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                    Properties.Settings.Default.FlightInfoPort);
            TcpListener server = new TcpListener(ep);

            server.Start();

            TcpClient clientSocket = server.AcceptTcpClient();
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);

            DateTime start = DateTime.UtcNow;


            string inputLine;
            string[] splitted;

            while (!shouldStop)
            {
                inputLine = readUntilNewLine(reader);

                if (Convert.ToInt32((DateTime.UtcNow - start).TotalSeconds) < 90)
                {
                    continue;
                }

                splitted = inputLine.Split(',');
                Lon = float.Parse(splitted[0]);
                Lat = float.Parse(splitted[1]);
                Console.WriteLine("Lon {0} Lat {1}", Lon, Lat);
                //Thread.Sleep(250);
            }

            clientSocket.Close();
            server.Stop();

        }
    }
}
