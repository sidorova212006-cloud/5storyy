using System;
using System.Windows.Input;
using plug.Services;

namespace plug.Commands
{
    /// <summary>
    /// Command: рассчитывает кол-во упаковок плитки.
    /// Формула: packages = ceil(area * 1.1 / m2InPackage) (добавляем +10% запас).
    /// </summary>
    public class CalculateTileCommand : ICommand
    {
        private readonly Func<double> _getArea;
        private readonly Func<double> _getM2InPackage;
        private readonly Func<double, string> _formatResult;
        private readonly Action<string> _setResult;
        private readonly Action<string> _addHistory;

        public CalculateTileCommand(
            Func<double> getArea,
            Func<double> getM2InPackage,
            Func<double, string> formatResult,
            Action<string> setResult,
            Action<string> addHistory)
        {
            _getArea = getArea;
            _getM2InPackage = getM2InPackage;
            _formatResult = formatResult;
            _setResult = setResult;
            _addHistory = addHistory;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var result = ConstructionMath.TilePackages(_getArea(), _getM2InPackage());
            var text = $"{_formatResult(result)} упаковок";
            _setResult(text);
            _addHistory($"Плитка: {text}");
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
