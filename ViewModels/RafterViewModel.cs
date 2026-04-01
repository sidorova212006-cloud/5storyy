using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class RafterViewModel : INotifyPropertyChanged
    {
        private readonly RafterModel _model = new RafterModel();

        public double HorizontalProjection
        {
            get => _model.CalcHorizontalProjection;
            set { _model.CalcHorizontalProjection = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
        }

        public double AngleDegrees
        {
            get => _model.CalcAngleDegrees;
            set { _model.CalcAngleDegrees = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
        }

        public string RafterLength => (_model.CalcAngleDegrees > 0 && _model.CalcAngleDegrees < 90)
            ? $"{ConstructionMath.RafterLength(_model.CalcHorizontalProjection, _model.CalcAngleDegrees):F2} м"
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


