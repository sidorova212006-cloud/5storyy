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
            get => _model.Length;
            set { _model.Length = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public double Width
        {
            get => _model.Width;
            set { _model.Width = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public double Depth
        {
            get => _model.Depth;
            set { _model.Depth = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
        }

        public string ConcreteVolume =>
            $"{ConstructionMath.ConcreteVolumeSlab(_model.Length, _model.Width, _model.Depth):F3} м³";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}
