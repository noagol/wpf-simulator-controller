using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {

        /// <summary>Gets or sets the aileron.</summary>
        /// <value>The aileron.</value>
        public double Aileron { get; set; }

        /// <summary>Gets or sets the elevator.</summary>
        /// <value>The elevator.</value>
        public double Elevator { get; set; }
    }
}
