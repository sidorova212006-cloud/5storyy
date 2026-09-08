using System;
using System.Windows.Input;

namespace plug.Commands
{
    /// <summary>
    /// Command: очищает историю результатов калькулятора.
    /// </summary>
    public class ClearHistoryCommand : ICommand
    {
        private readonly Action _execute;

        public ClearHistoryCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _execute();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
