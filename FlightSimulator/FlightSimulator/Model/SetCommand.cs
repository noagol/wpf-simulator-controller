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


        /// <summary>Initializes a new instance of the <see cref="SetCommand"/> class.</summary>
        /// <param name="cmnd">The CMND.</param>
        /// <param name="delay">if set to <c>true</c> [delay].</param>
        public SetCommand(string cmnd, bool delay)
        {
            command = cmnd;
            shouldDelay = delay;
        }


        /// <summary>Gets or sets the command to send.</summary>
        /// <value>The command.</value>
        public string Command
        {
            get { return command; }
            set { command = value; }
        }


        /// <summary>Gets or sets a value indicating whether [should delay].</summary>
        /// <value>
        ///   <c>true</c> if [should delay]; otherwise, <c>false</c>.</value>
        public bool ShouldDelay
        {
            get { return shouldDelay; }
            set { shouldDelay = value; }
        }
    }
}
