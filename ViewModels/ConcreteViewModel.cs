using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class ConcreteViewModel : INotifyPropertyChanged
    {
        private readonly ConcreteModel _model = new ConcreteModel();

        public double Length
        {
            get => _model.CalcLength;
            set { _model.CalcLength = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public double Width
        {
            get => _model.CalcWidth;
            set { _model.CalcWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public double Depth
        {
            get => _model.CalcDepth;
            set { _model.CalcDepth = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public string ConcreteVolume =>
            $"{ConstructionMath.ConcreteVolumeSlab(_model.CalcLength, _model.CalcWidth, _model.CalcDepth):F3} м³";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


