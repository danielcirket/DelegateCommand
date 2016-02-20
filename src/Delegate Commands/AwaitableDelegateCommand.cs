using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public class AwaitableDelegateCommand : AwaitableDelegateCommand<object>
    {
        public AwaitableDelegateCommand(Func<Task> execute) : this(execute, () => true) { }
        public AwaitableDelegateCommand(Func<Task> execute, Func<bool> canExecute) : base(x => execute(), x => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }
    }

    public class AwaitableDelegateCommand<T> : IAwaitableCommand, ICommand
    {
        private readonly Func<T, Task> _execute;
        private readonly DelegateCommand<T> _delegateCommand;
        private readonly CancellationToken _cancellationToken;
        private bool _isExecuting;

        public AwaitableDelegateCommand(Func<T, Task> execute) : this(execute, (T) => true) { }
        public AwaitableDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute) : this(execute, canExecute, CancellationToken.None) { }
        public AwaitableDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute, CancellationToken cancellationToken)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _delegateCommand = new DelegateCommand<T>(x => { }, canExecute);
            _cancellationToken = cancellationToken;
        }

        #region IAsyncCommand Implementation

        public event EventHandler CanExecuteChanged
        {
            add { _delegateCommand.CanExecuteChanged += value; }
            remove { _delegateCommand.CanExecuteChanged -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && _delegateCommand.CanExecute((T)parameter); ;
        }
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public async Task ExecuteAsync(object parameter)
        {
            _isExecuting = true;

            RaiseCanExecuteChanged();

            await _execute((T)parameter);

            _isExecuting = false;

            RaiseCanExecuteChanged();
        }
        public void RaiseCanExecuteChanged()
        {
            _delegateCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
