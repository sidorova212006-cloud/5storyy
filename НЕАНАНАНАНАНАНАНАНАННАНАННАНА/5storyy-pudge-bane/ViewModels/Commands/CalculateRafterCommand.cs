using System;
using System.Windows.Input;
using plug.Services;

namespace plug.Commands
{
    /// <summary>
    /// Command: рассчитывает длину стропила по проекции и углу:
    /// hypotenuse = projection / cos(angle).
    /// </summary>
    public class CalculateRafterCommand : ICommand
    {
        private readonly Func<double> _getProjection;
        private readonly Func<double> _getAngleDegrees;
        private readonly Func<double, string> _formatResult;
        private readonly Action<string> _setResult;
        private readonly Action<string> _addHistory;

        public CalculateRafterCommand(
            Func<double> getProjection,
            Func<double> getAngleDegrees,
            Func<double, string> formatResult,
            Action<string> setResult,
            Action<string> addHistory)
        {
            _getProjection = getProjection;
            _getAngleDegrees = getAngleDegrees;
            _formatResult = formatResult;
            _setResult = setResult;
            _addHistory = addHistory;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var result = ConstructionMath.RafterLength(_getProjection(), _getAngleDegrees());
            var text = $"{_formatResult(result)} м";
            _setResult(text);
            _addHistory($"Стропило: {text}");
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
