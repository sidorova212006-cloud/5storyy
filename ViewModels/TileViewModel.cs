using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class TileViewModel : INotifyPropertyChanged
    {
        private readonly TileModel _model = new TileModel { CalcM2InPackage = 1.5 };

        public double Area
        {
            get => _model.CalcArea;
            set { _model.CalcArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
        }

        public double M2InPackage
        {
            get => _model.CalcM2InPackage;
            set { _model.CalcM2InPackage = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
        }

        public string TotalPackages => _model.CalcM2InPackage > 0
            ? $"{ConstructionMath.TilePackages(_model.CalcArea, _model.CalcM2InPackage)} уп."
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


