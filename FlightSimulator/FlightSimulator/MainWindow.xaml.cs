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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        /// <summary>Initializes a new instance of the <see cref="MainWindow"/> class.</summary>
        public MainWindow()
        {
            InitializeComponent();
            // Initiaize view model
            vm = new MainWindowViewModel();
            // Set data context
            this.DataContext = vm;
        }
    }
}
