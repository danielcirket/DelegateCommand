using System;
using System.Windows.Input;

namespace Implementations
{
    public class DelegateCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion

        #region Constructor

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute cannot be null");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public void RaiseCanExecuteChanged()
        {

            if (this.CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
