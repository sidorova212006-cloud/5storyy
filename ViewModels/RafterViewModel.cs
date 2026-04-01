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
            get => _model.HorizontalProjection;
            set { _model.HorizontalProjection = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
        }

        public double AngleDegrees
        {
            get => _model.AngleDegrees;
            set { _model.AngleDegrees = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
        }

        public string RafterLength => (_model.AngleDegrees > 0 && _model.AngleDegrees < 90)
            ? $"{ConstructionMath.RafterLength(_model.HorizontalProjection, _model.AngleDegrees):F2} м"
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}
