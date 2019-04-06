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
    }
}
