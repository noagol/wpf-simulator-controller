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
        public FlightBoardViewModel()
        {
            sm = new ServerModel();
            sm.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }


        public double Lon
        {
            get { return sm.Lon; }
        }

        public double Lat
        {
            get { return sm.Lat; }
        }
    }
}
