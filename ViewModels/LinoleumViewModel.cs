using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class LinoleumViewModel : INotifyPropertyChanged
    {
        private readonly LinoleumModel _model = new LinoleumModel { CalcWidth = 1.5 };

        public double Area
        {
            get => _model.CalcArea;
            set { _model.CalcArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
        }

        public double Width
        {
            get => _model.CalcWidth;
            set { _model.CalcWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
        }

        public string LinearMeters => _model.CalcWidth > 0
            ? $"{ConstructionMath.SqMetToLinear(_model.CalcArea, _model.CalcWidth):F2} пог. м"
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}


