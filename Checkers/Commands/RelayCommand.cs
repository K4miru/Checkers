using System;
using System.Windows.Input;

namespace Checkers
{
    public class RelayCommand : ICommand
    {
        private bool canExecute;
        private Action execute;

        public RelayCommand(Action execute)
            : this(execute, true)
        { }

        public RelayCommand(Action execute, bool canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}