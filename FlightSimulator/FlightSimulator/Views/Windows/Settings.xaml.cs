﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightSimulator.ViewModels.Windows;
namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private SettingsWindowViewModel vm;

        /// <summary>Initializes a new instance of the <see cref="Settings"/> class.</summary>
        public Settings()
        {
            InitializeComponent();
            // Initialize view model
            vm = new SettingsWindowViewModel();
            // Set data context
            this.DataContext = vm;
            // Enable close view from view model
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }
    }
}
