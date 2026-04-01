using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class WallViewModel : INotifyPropertyChanged
    {
        private readonly WallModel _model = new WallModel();

        public double Length
        {
            get => _model.Length;
            set { _model.Length = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double Width
        {
            get => _model.Width;
            set { _model.Width = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double Height
        {
            get => _model.Height;
            set { _model.Height = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double OpeningsArea
        {
            get => _model.OpeningsArea;
            set { _model.OpeningsArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public string WallAreaResult =>
            $"{ConstructionMath.WallArea(_model.Length, _model.Width, _model.Height, _model.OpeningsArea):F2} м²";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}
