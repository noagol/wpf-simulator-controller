using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Views.Windows;
using System.Windows.Media;

namespace FlightSimulator.ViewModels.Windows
{
    class MainWindowViewModel : BaseNotify
    {
        private ConnectModel cm;
        private string _autoPilotText;
        private Brush _autoPilotBackground;

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
                // First connect
                cm = new ConnectModel();
            }
            else if (!cm.IP.Equals(Properties.Settings.Default.FlightServerIP)
                || !cm.Port.Equals(Properties.Settings.Default.FlightCommandPort))
            {
                // Values changed
                // Stop running thread
                cm.stop();
                // Create new connect model
                cm = new ConnectModel();
            }
        }
        #endregion

        #region ClearCommand
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClearCommand()));
            }
        }

        private void OnClearCommand()
        {
            AutoPilotText = "";
        }
        #endregion

        #region SendCommandsCommand
        private ICommand _sendCommandsCommand;
        public ICommand SendCommandsCommand
        {
            get
            {
                return _sendCommandsCommand ?? (_sendCommandsCommand = new CommandHandler(() => OnSendCommandsCommand()));
            }
        }

        private void OnSendCommandsCommand()
        {
            if (cm != null && AutoPilotText != "")
            {
                string[] splitted = AutoPilotText.Split('\n');
                foreach (string s in splitted)
                {
                    SetCommand setCommand = new SetCommand(s.Trim(), true);
                    cm.addCommand(setCommand);
                }
                AutoPilotBackground = Brushes.White;
            }
        }

        public string AutoPilotText
        {
            get
            {
                return _autoPilotText;
            }
            set
            {
                _autoPilotText = value;
                if (value == "")
                {
                    AutoPilotBackground = Brushes.White;
                }
                else
                {
                    AutoPilotBackground = Brushes.LightPink;
                }
                NotifyPropertyChanged("AutoPilotText");
            }
        }

        public Brush AutoPilotBackground
        {
            get
            {
                return _autoPilotBackground;
            }
            set
            {
                _autoPilotBackground = value;
                NotifyPropertyChanged("AutoPilotBackground");
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
            get
            {
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
            get
            {
                if (cm != null)
                    return cm.Aileron;
                else
                    return 0.0f;
            }
            set
            {
                if (cm != null)
                {
                    cm.Aileron = value / 124.0f;
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
                    cm.Elevator = value / 124.0f;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }
    }
}
