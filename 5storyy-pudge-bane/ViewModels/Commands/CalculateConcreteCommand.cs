using System;
using System.Windows.Input;
using plug.Services;

namespace plug.Commands
{
    /// <summary>
    /// Command: рассчитывает объём бетона (м³) по формуле L × W × D.
    /// Делегирует вычисление в <see cref="plug.Services.ConstructionMath.ConcreteVolume(double, double, double)"/>.
    /// </summary>
    public class CalculateConcreteCommand : ICommand
    {
        private readonly Func<double> _getLength;
        private readonly Func<double> _getWidth;
        private readonly Func<double> _getDepth;
        private readonly Func<double, string> _formatResult;
        private readonly Action<string> _setResult;
        private readonly Action<string> _addHistory;

        public CalculateConcreteCommand(
            Func<double> getLength,
            Func<double> getWidth,
            Func<double> getDepth,
            Func<double, string> formatResult,
            Action<string> setResult,
            Action<string> addHistory)
        {
            _getLength = getLength;
            _getWidth = getWidth;
            _getDepth = getDepth;
            _formatResult = formatResult;
            _setResult = setResult;
            _addHistory = addHistory;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var result = ConstructionMath.ConcreteVolume(_getLength(), _getWidth(), _getDepth());
            var text = $"{_formatResult(result)} м³";
            _setResult(text);
            _addHistory($"Бетон: {text}");
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
