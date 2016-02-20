using System;
using System.Windows.Input;

namespace Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action execute) : this(execute, () => true) { }
        public DelegateCommand(Action execute, Func<bool> canExecute) : base(x => execute(), x => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute) : this(execute, (T) => true)
        {

        }
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T)parameter);
        }
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
