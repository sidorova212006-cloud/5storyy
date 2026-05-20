using System;
using System.Windows.Input;
using plug.Services;

namespace plug.Commands
{
    /// <summary>
    /// Command: рассчитывает линейные метры (п.м.) для линолеума по формуле
    /// L = area / width (delegates to <see cref="plug.Services.ConstructionMath.LinearMeters(double, double)"/>).
    /// </summary>
    public class CalculateLinoleumCommand : ICommand
    {
        private readonly Func<double> _getArea;
        private readonly Func<double> _getWidth;
        private readonly Func<double, string> _formatResult;
        private readonly Action<string> _setResult;
        private readonly Action<string> _addHistory;

        public CalculateLinoleumCommand(
            Func<double> getArea,
            Func<double> getWidth,
            Func<double, string> formatResult,
            Action<string> setResult,
            Action<string> addHistory)
        {
            _getArea = getArea;
            _getWidth = getWidth;
            _formatResult = formatResult;
            _setResult = setResult;
            _addHistory = addHistory;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var result = ConstructionMath.LinearMeters(_getArea(), _getWidth());
            var text = $"{_formatResult(result)} п.м.";
            _setResult(text);
            _addHistory($"Линолеум: {text}");
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
