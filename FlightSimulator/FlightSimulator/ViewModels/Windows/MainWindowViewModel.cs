using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Views.Windows;

namespace FlightSimulator.ViewModels.Windows
{
    class MainWindowViewModel : BaseNotify
    {
        private ConnectModel cm;

        public MainWindowViewModel()
        {
            cm = null;
        }

        #region OpenSettingsCommand
        private ICommand _settingsCommand;
        public ICommand OpenSettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnOpenSettings()));
            }
        }

        private void OnOpenSettings()
        {
            Settings s = new Settings();
            s.Show();
        }
        #endregion

        #region ConnectCommand
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnectCommand()));
            }
        }

        private void OnConnectCommand()
        {
            if (cm == null)
            {
                cm = new ConnectModel();
            }
        }
        #endregion

        public float Throttle
        {
            get
            {
                if (cm != null)
                    return cm.Throttle;
                else
                    return 0.0f;
            }
            set
            {
                if (cm != null)
                {
                    cm.Throttle = value;
                    NotifyPropertyChanged("Throttle");
                }
            }
        }

        public float Rudder
        {
            get {
                if (cm != null)
                    return cm.Rudder;
                else
                    return 0.0f;
            }
            set
            {
                if (cm != null)
                {
                    cm.Rudder = value;
                    NotifyPropertyChanged("Rudder");
                }
            }
        }

        public float Aileron
        {
            get {
                if (cm != null)
                    return cm.Aileron;
                else
                    return 0.0f;
            }
            set
            {
                if (cm != null)
                {
                    cm.Aileron = value;
                    NotifyPropertyChanged("Aileron");
                }
            }
        }

        public float Elevator
        {
            get
            {
                if (cm != null)
                    return cm.Elevator;
                else
                    return 0.0f;
            }
            set
            {
                if (cm != null)
                {
                    cm.Elevator = value;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }
    }
}
