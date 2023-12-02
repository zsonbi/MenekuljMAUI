#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Menekulj.ViewModel
{
    public class DelegateCommand : ICommand
    {

        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? canExecute;
        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<object?> executeAction)
        {
            _execute = executeAction;
        }

        public DelegateCommand(Func<object?, bool> canExecute, Action<object?> executeAction)
        {
            this.canExecute = canExecute;
            this._execute = executeAction;

        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }


        public void RaiseCanExecuteChanged()
        {

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            else
            {
               return this.canExecute.Invoke(parameter);
            }
        }
    }
}
