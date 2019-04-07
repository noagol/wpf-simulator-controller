using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        public Action CloseAction { get; set; }


        /// <summary>Initializes a new instance of the <see cref="SettingsWindowViewModel"/> class.</summary>
        public SettingsWindowViewModel()
        {
            this.model = ApplicationSettingsModel.Instance;
        }


        /// <summary>Gets or sets the flight server ip.</summary>
        /// <value>The flight server ip.</value>
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }


        /// <summary>Gets or sets the flight command port.</summary>
        /// <value>The flight command port.</value>
        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }


        /// <summary>Gets or sets the flight information port.</summary>
        /// <value>The flight information port.</value>
        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }



        /// <summary>Saves the settings.</summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }


        /// <summary>Reloads the settings.</summary>
        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        #region Commands
        #region SaveCommand
        private ICommand _saveCommand;

        /// <summary>Gets the save command.</summary>
        /// <value>The save command.</value>
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => OnSave()));
            }
        }

        /// <summary>Called when [save].</summary>
        private void OnSave()
        {
            model.SaveSettings();
            CloseAction();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;

        /// <summary>Gets the cancel command.</summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }


        /// <summary>Called when [cancel].</summary>
        private void OnCancel()
        {
            model.ReloadSettings();
            CloseAction();
        }
        #endregion
        #endregion
    }
}

