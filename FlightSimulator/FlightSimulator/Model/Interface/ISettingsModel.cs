using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface ISettingsModel
    {

        /// <summary>Gets or sets the flight server ip.</summary>
        /// <value>The flight server ip.</value>
        string FlightServerIP { get; set; }


        /// <summary>Gets or sets the flight information port.</summary>
        /// <value>The flight information port.</value>
        int FlightInfoPort { get; set; }


        /// <summary>Gets or sets the flight command port.</summary>
        /// <value>The flight command port.</value>
        int FlightCommandPort { get; set; }           // The Port of the Flight Server


        /// <summary>Saves the settings.</summary>
        void SaveSettings();

        /// <summary>Reloads the settings.</summary>
        void ReloadSettings();
    }
}
