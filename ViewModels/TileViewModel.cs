using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class TileViewModel : INotifyPropertyChanged
    {
        private readonly TileModel _model = new TileModel { M2InPackage = 1.5 };

        public double Area
        {
            get => _model.Area;
            set { _model.Area = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
        }

        public double M2InPackage
        {
            get => _model.M2InPackage;
            set { _model.M2InPackage = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
        }

        public string TotalPackages => _model.M2InPackage > 0
            ? $"{ConstructionMath.TilePackages(_model.Area, _model.M2InPackage)} уп."
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


