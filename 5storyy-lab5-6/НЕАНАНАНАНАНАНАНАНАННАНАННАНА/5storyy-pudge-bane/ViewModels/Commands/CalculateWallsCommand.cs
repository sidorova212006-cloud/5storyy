using System;
using System.Windows.Input;
using plug.Models;
using plug.Services;

namespace plug.Commands
{
    /// <summary>
    /// Command: рассчитывает площадь стен (м²):
    /// (длина + ширина) × 2 × высота − площадь проёмов.
    /// </summary>
    public class CalculateWallsCommand : ICommand
    {
        private readonly Func<WallInput> _getInput;
        private readonly Func<double, string> _formatResult;
        private readonly Action<string> _setResult;
        private readonly Action<string> _addHistory;

        public CalculateWallsCommand(
            Func<WallInput> getInput,
            Func<double, string> formatResult,
            Action<string> setResult,
            Action<string> addHistory)
        {
            _getInput = getInput;
            _formatResult = formatResult;
            _setResult = setResult;
            _addHistory = addHistory;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var input = _getInput();
            var result = ConstructionMath.WallArea(input.Length, input.Width, input.Height, input.OpeningsArea);
            var text = $"{_formatResult(result)} м²";
            _setResult(text);
            _addHistory($"Стены: {text}");
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
