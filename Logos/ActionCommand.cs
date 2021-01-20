using System;
using System.Windows.Input;

namespace Logos
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> canExecute;

        public ActionCommand(Action<object> action, Predicate<object> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute is null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
