using System;
using Commands;
using FluentAssertions;
using Xunit;

namespace DelegateCommands.Tests
{
    public class DelegateCommandTests
    {
        [Fact]
        public static void WhenNullExecuteMethodThenShouldThrowArgumentNullException()
        {
            DelegateCommand delegateCommand = null;

            Action act = () => delegateCommand = new DelegateCommand(null);

            act.ShouldThrow<ArgumentNullException>();
        }
        [Fact]
        public static void WhenNonNullExecuteAndNullCanExecuteThenShouldNotThrowArgumentNullException()
        {
            DelegateCommand delegateCommand = null;

            Action act = () => delegateCommand = new DelegateCommand(() => { }, null);

            act.ShouldNotThrow<ArgumentNullException>();
        }
        [Fact]
        public static void WhenNonNullExecuteAndNullCanExecuteThenCanExecuteMethodShouldReturnTrue()
        {
            var delegateCommand = new DelegateCommand(() => { });

            delegateCommand.CanExecute(null).Should().Be(true);
        }
        [Fact]
        public static void WhenCanExecuteFuncReturnsFalseThenCanExecuteMethodShouldReturnFalse()
        {
            var delegateCommand = new DelegateCommand(() => { }, () => false);

            delegateCommand.CanExecute(null).Should().Be(false);
        }
        [Fact]
        public static void WhenCanExecuteFuncReturnsTrueThenCanExecuteMethodShouldReturnTrue()
        {
            var delegateCommand = new DelegateCommand(() => { }, () => true);

            delegateCommand.CanExecute(null).Should().Be(true);
        }
        [Fact]
        public static void WhenExecutedThenExecuteParameterShouldBeCalled()
        {
            var hasRun = false;

            var delegateCommand = new DelegateCommand(() => hasRun = true);

            delegateCommand.Execute(null);

            hasRun.Should().Be(true);
        }
    }
}
