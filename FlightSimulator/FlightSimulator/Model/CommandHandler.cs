﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand
    {
        private Action _action;

        /// <summary>Initializes a new instance of the <see cref="CommandHandler"/> class.</summary>
        /// <param name="action">The action.</param>
        public CommandHandler(Action action)
        {
            _action = action;
        }


        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }


        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
