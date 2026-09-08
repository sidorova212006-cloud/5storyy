using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using plug.Commands;
using plug.Models;
using plug.Services;

namespace plug.ViewModels
{
    /// <summary>
    /// ViewModel слоя MVVM. Реализует <see cref="INotifyPropertyChanged"/> (Observer),
    /// уведомляя WPF о смене свойств.
    /// Создаёт Command-объекты и делегирует расчёты в <see cref="plug.Services.ConstructionMath"/>
    /// через них.
    /// </summary>
    public class ConstructionViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorSettings _settings;

        private string _linoleumArea = string.Empty;
        private string _linoleumWidth = string.Empty;
        private string _linoleumResult = string.Empty;

        private string _wallLength = string.Empty;
        private string _wallWidth = string.Empty;
        private string _wallHeight = string.Empty;
        private string _wallOpeningsArea = string.Empty;
        private string _wallAreaResult = string.Empty;

        private string _concreteLength = string.Empty;
        private string _concreteWidth = string.Empty;
        private string _concreteDepth = string.Empty;
        private string _concreteVolumeResult = string.Empty;

        private string _tileArea = string.Empty;
        private string _tileM2InPackage = string.Empty;
        private string _tilePackagesResult = string.Empty;

        private string _rafterProjection = string.Empty;
        private string _rafterAngleDegrees = string.Empty;
        private string _rafterLengthResult = string.Empty;

        public ConstructionViewModel()
        {
            _settings = CalculatorSettings.GetInstance();

            CalculateLinoleumCommand = new CalculateLinoleumCommand(
                () => NumberHelper.ParseDouble(LinoleumArea),
                () => NumberHelper.ParseDouble(LinoleumWidth),
                value => NumberHelper.FormatResult(value, _settings.Precision),
                text => LinoleumResult = text,
                AddHistory);

            CalculateWallsCommand = new CalculateWallsCommand(
                BuildWallInput,
                value => NumberHelper.FormatResult(value, _settings.Precision),
                text => WallAreaResult = text,
                AddHistory);

            CalculateConcreteCommand = new CalculateConcreteCommand(
                () => NumberHelper.ParseDouble(ConcreteLength),
                () => NumberHelper.ParseDouble(ConcreteWidth),
                () => NumberHelper.ParseDouble(ConcreteDepth),
                value => NumberHelper.FormatResult(value, _settings.Precision),
                text => ConcreteVolumeResult = text,
                AddHistory);

            CalculateTileCommand = new CalculateTileCommand(
                () => NumberHelper.ParseDouble(TileArea),
                () => NumberHelper.ParseDouble(TileM2InPackage),
                value => NumberHelper.FormatResult(value, _settings.Precision),
                text => TilePackagesResult = text,
                AddHistory);

            CalculateRafterCommand = new CalculateRafterCommand(
                () => NumberHelper.ParseDouble(RafterProjection),
                () => NumberHelper.ParseDouble(RafterAngleDegrees),
                value => NumberHelper.FormatResult(value, _settings.Precision),
                text => RafterLengthResult = text,
                AddHistory);

            ClearHistoryCommand = new ClearHistoryCommand(ClearHistory);
        }

        public string LinoleumArea
        {
            get => _linoleumArea;
            set => SetProperty(ref _linoleumArea, value);
        }

        public string LinoleumWidth
        {
            get => _linoleumWidth;
            set => SetProperty(ref _linoleumWidth, value);
        }

        public string LinoleumResult
        {
            get => _linoleumResult;
            set => SetProperty(ref _linoleumResult, value);
        }

        public string WallLength
        {
            get => _wallLength;
            set => SetProperty(ref _wallLength, value);
        }

        public string WallWidth
        {
            get => _wallWidth;
            set => SetProperty(ref _wallWidth, value);
        }

        public string WallHeight
        {
            get => _wallHeight;
            set => SetProperty(ref _wallHeight, value);
        }

        public string WallOpeningsArea
        {
            get => _wallOpeningsArea;
            set => SetProperty(ref _wallOpeningsArea, value);
        }

        public string WallAreaResult
        {
            get => _wallAreaResult;
            set => SetProperty(ref _wallAreaResult, value);
        }

        public string ConcreteLength
        {
            get => _concreteLength;
            set => SetProperty(ref _concreteLength, value);
        }

        public string ConcreteWidth
        {
            get => _concreteWidth;
            set => SetProperty(ref _concreteWidth, value);
        }

        public string ConcreteDepth
        {
            get => _concreteDepth;
            set => SetProperty(ref _concreteDepth, value);
        }

        public string ConcreteVolumeResult
        {
            get => _concreteVolumeResult;
            set => SetProperty(ref _concreteVolumeResult, value);
        }

        public string TileArea
        {
            get => _tileArea;
            set => SetProperty(ref _tileArea, value);
        }

        public string TileM2InPackage
        {
            get => _tileM2InPackage;
            set => SetProperty(ref _tileM2InPackage, value);
        }

        public string TilePackagesResult
        {
            get => _tilePackagesResult;
            set => SetProperty(ref _tilePackagesResult, value);
        }

        public string RafterProjection
        {
            get => _rafterProjection;
            set => SetProperty(ref _rafterProjection, value);
        }

        public string RafterAngleDegrees
        {
            get => _rafterAngleDegrees;
            set => SetProperty(ref _rafterAngleDegrees, value);
        }

        public string RafterLengthResult
        {
            get => _rafterLengthResult;
            set => SetProperty(ref _rafterLengthResult, value);
        }

        public ICommand CalculateLinoleumCommand { get; }
        public ICommand CalculateWallsCommand { get; }
        public ICommand CalculateConcreteCommand { get; }
        public ICommand CalculateTileCommand { get; }
        public ICommand CalculateRafterCommand { get; }
        public ICommand ClearHistoryCommand { get; }

        public string History => string.Join("\n", _settings.LastResults);

        private WallInput BuildWallInput()
        {
            return new WallInputBuilder()
                .SetLength(NumberHelper.ParseDouble(WallLength))
                .SetWidth(NumberHelper.ParseDouble(WallWidth))
                .SetHeight(NumberHelper.ParseDouble(WallHeight))
                .SetOpenings(NumberHelper.ParseDouble(WallOpeningsArea))
                .Build();
        }

        private void AddHistory(string entry)
        {
            _settings.AddResult(entry);
            OnPropertyChanged(nameof(History));
        }

        private void ClearHistory()
        {
            _settings.ClearHistory();
            OnPropertyChanged(nameof(History));
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
