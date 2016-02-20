#Delegate Command Implementations

## Examples:

ICommand:

```C#
public ICommand MyCommand { get; protected set; }

MyCommand = new DelegateCommand(x => MyAction());
MyCommand = new DelegateCommand(x => MyAction(), y => CanExecute());

MyCommand = new DelegateCommand<T>(x => MyActionWithParameter(x));
MyCommand = new DelegateCommand<T>(x => MyActionWithParameter(x), y => CanExecute());

```
```C#

private void MyAction()
{
    // Do stuff
}

private void MyActionWithParameter(T param)
{
    // Do stuff
}

```
IAwaitableCommand:

```C#
public IAwaitableCommand MyAwaitableCommand { get; protected set; }

MyAwaitableCommand = new AwaitableDelegateCommand(x => MyAction());
MyAwaitableCommand = new AwaitableDelegateCommand(x => MyAction(), y => CanExecute());

MyAwaitableCommand = new AwaitableDelegateCommand<T>(x => MyActionWithParameter(x));
MyAwaitableCommand = new AwaitableDelegateCommand<T>(x => MyActionWithParameter(x), y => CanExecute());

```
```C#
private Task MyAction()
{
    await SomeAsyncStuff();
}

private Task MyActionWithParameter(T param)
{
    await SomeAsyncStuff(param);
}

```

