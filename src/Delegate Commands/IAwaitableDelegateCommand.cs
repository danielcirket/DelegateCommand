using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public interface IAwaitableCommand : IAwaitableCommand<object>
    {
    }

    public interface IAwaitableCommand<in T>
    {
        Task ExecuteAsync(T obj);
        void RaiseCanExecuteChanged();
    }
}
