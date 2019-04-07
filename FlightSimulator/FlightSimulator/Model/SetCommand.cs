using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class SetCommand
    {
        private string command;
        private bool shouldDelay;

        public SetCommand(string cmnd, bool delay)
        {
            command = cmnd;
            shouldDelay = delay;
        }

        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        public bool ShouldDelay
        {
            get { return shouldDelay; }
            set { shouldDelay = value; }
        }
    }
}
