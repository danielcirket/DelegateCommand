using System;
using System.Threading.Tasks;
using Commands;
using FluentAssertions;
using Xunit;

namespace DelegateCommands.Tests
{
    public class AwaitableDelegateCommandTests
    {
        [Fact]
        public static void WhenNullExecuteMethodThenShouldThrowArgumentNullException()
        {
            AwaitableDelegateCommand delegateCommand = null;

            Action act = () => delegateCommand = new AwaitableDelegateCommand(null);

            act.ShouldThrow<ArgumentNullException>();
        }
        [Fact]
        public static void WhenNonNullExecuteAndNullCanExecuteThenShouldNotThrowArgumentNullException()
        {
            AwaitableDelegateCommand delegateCommand = null;

            Action act = () => delegateCommand = new AwaitableDelegateCommand(() => new Task(() => { }), null);

            act.ShouldNotThrow<ArgumentNullException>();
        }
        [Fact]
        public static void WhenNonNullExecuteAndNullCanExecuteThenCanExecuteMethodShouldReturnTrue()
        {
            var delegateCommand = new AwaitableDelegateCommand(() => new Task(() => { }));

            delegateCommand.CanExecute(null).Should().Be(true);
        }
        [Fact]
        public static void WhenCanExecuteFuncReturnsFalseThenCanExecuteMethodShouldReturnFalse()
        {
            var delegateCommand = new AwaitableDelegateCommand(() => new Task(() => { }), () => false);

            delegateCommand.CanExecute(null).Should().Be(false);
        }
        [Fact]
        public static void WhenCanExecuteFuncReturnsTrueThenCanExecuteMethodShouldReturnTrue()
        {
            var delegateCommand = new AwaitableDelegateCommand(() => new Task(() => { }), () => true);

            delegateCommand.CanExecute(null).Should().Be(true);
        }
        [Fact]
        public async static Task WhenExecutedThenExecuteParameterShouldBeCalled()
        {
            var hasRun = false;

            IAwaitableCommand delegateCommand = new AwaitableDelegateCommand(() => Task.Run(() => hasRun = true));

            await delegateCommand.ExecuteAsync(null);

            hasRun.Should().Be(true);
        }
    }
}
