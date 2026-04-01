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
            get => _model.CalcLength;
            set { _model.CalcLength = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double Width
        {
            get => _model.CalcWidth;
            set { _model.CalcWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double Height
        {
            get => _model.CalcHeight;
            set { _model.CalcHeight = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public double OpeningsArea
        {
            get => _model.CalcOpeningsArea;
            set { _model.CalcOpeningsArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
        }

        public string WallAreaResult =>
            $"{ConstructionMath.WallArea(_model.CalcLength, _model.CalcWidth, _model.CalcHeight, _model.CalcOpeningsArea):F2} м²";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


