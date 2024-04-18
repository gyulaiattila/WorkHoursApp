namespace WorkHoursApp.Command
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Class for DelegateCommand.
    /// </summary>
    public class Command : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => _canExecute is null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}
