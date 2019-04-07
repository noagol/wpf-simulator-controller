using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private ServerModel sm;

        /// <summary>Initializes a new instance of the <see cref="FlightBoardViewModel"/> class.</summary>
        public FlightBoardViewModel()
        {
            sm = new ServerModel();
            sm.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>Called when [property changed].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        /// <summary>Gets the lon.</summary>
        /// <value>The lon.</value>
        public double Lon
        {
            get { return sm.Lon; }
        }

        /// <summary>Gets the lat.</summary>
        /// <value>The lat.</value>
        public double Lat
        {
            get { return sm.Lat; }
        }
    }
}
