using System;
using System.Windows.Input;

namespace Implementations
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private readonly Action<object> _executeWithParam;
        private readonly Predicate<object> _canExecuteWithParam;

        public DelegateCommand(Action execute) : this(execute, () => true) { }
        public DelegateCommand(Action<object> execute) : this(execute, null) { }
        public DelegateCommand(Action execute, Func<bool> canExecute) 
        {
            if (execute == null)
                throw new ArgumentNullException("Execute", "Execute cannot be null");

            _execute = execute;
            _canExecute = canExecute;
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute", "Execute cannot be null");

            _executeWithParam = execute;
            _canExecuteWithParam = canExecute;
        }

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecuteWithParam == null && _canExecute == null)
            {
                return true;
            }
            else if (_canExecute != null)
            {
                return _canExecute();
            }

            return _canExecuteWithParam(parameter);
        }
        public void Execute(object parameter)
        {
            if (_executeWithParam == null)
            {
                _execute();
                return;
            }

            _executeWithParam(parameter);
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
