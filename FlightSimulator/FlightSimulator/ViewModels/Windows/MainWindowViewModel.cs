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

        /// <summary>Initializes a new instance of the <see cref="MainWindowViewModel"/> class.</summary>
        public MainWindowViewModel()
        {
            cm = null;
        }

        #region OpenSettingsCommand
        private ICommand _settingsCommand;

        /// <summary>Gets the open settings command.</summary>
        /// <value>The open settings command.</value>
        public ICommand OpenSettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnOpenSettings()));
            }
        }


        /// <summary>Called when [open settings].</summary>
        private void OnOpenSettings()
        {
            // Start settings window
            Settings s = new Settings();
            s.Show();
        }
        #endregion

        #region ConnectCommand
        private ICommand _connectCommand;

        /// <summary>Gets the connect command.</summary>
        /// <value>The connect command.</value>
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnectCommand()));
            }
        }


        /// <summary>Called when [connect command].</summary>
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

        /// <summary>Gets the clear command.</summary>
        /// <value>The clear command.</value>
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClearCommand()));
            }
        }


        /// <summary>Called when [clear command].</summary>
        private void OnClearCommand()
        {
            // Put an empty string at the text box
            AutoPilotText = "";
        }
        #endregion

        #region SendCommandsCommand
        private ICommand _sendCommandsCommand;

        /// <summary>Gets the send commands command.</summary>
        /// <value>The send commands command.</value>
        public ICommand SendCommandsCommand
        {
            get
            {
                return _sendCommandsCommand ?? (_sendCommandsCommand = new CommandHandler(() => OnSendCommandsCommand()));
            }
        }

        /// <summary>Called when [send commands command].</summary>
        private void OnSendCommandsCommand()
        {
            if (cm != null && AutoPilotText != "")
            {
                // Split by new lien
                string[] splitted = AutoPilotText.Split('\n');
                // Add each command to the queue
                foreach (string s in splitted)
                {
                    SetCommand setCommand = new SetCommand(s.Trim(), true);
                    cm.addCommand(setCommand);
                }
                // Make text box background white
                AutoPilotBackground = Brushes.White;
            }
        }


        /// <summary>Gets or sets the automatic pilot text.</summary>
        /// <value>The automatic pilot text.</value>
        public string AutoPilotText
        {
            get
            {
                return _autoPilotText;
            }
            set
            {
                _autoPilotText = value;
                // Change the background color
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


        /// <summary>Gets or sets the automatic pilot background.</summary>
        /// <value>The automatic pilot background.</value>
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

        /// <summary>Gets or sets the throttle.</summary>
        /// <value>The throttle.</value>
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


        /// <summary>Gets or sets the rudder.</summary>
        /// <value>The rudder.</value>
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


        /// <summary>Gets or sets the aileron.</summary>
        /// <value>The aileron.</value>
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
                    // Normalize value
                    cm.Aileron = value / 124.0f;
                    NotifyPropertyChanged("Aileron");
                }
            }
        }


        /// <summary>Gets or sets the elevator.</summary>
        /// <value>The elevator.</value>
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
                    // Normalize value
                    cm.Elevator = value / 124.0f;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }
    }
}
