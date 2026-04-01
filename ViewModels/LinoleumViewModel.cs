using System.ComponentModel;
using System.Runtime.CompilerServices;
using plug.Models;

namespace plug.ViewModels
{
    public class LinoleumViewModel : INotifyPropertyChanged
    {
        private readonly LinoleumModel _model = new LinoleumModel { Width = 1.5 };

        public double Area
        {
            get => _model.Area;
            set { _model.Area = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
        }

        public double Width
        {
            get => _model.Width;
            set { _model.Width = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
        }

        public string LinearMeters => _model.Width > 0
            ? $"{ConstructionMath.SqMetToLinear(_model.Area, _model.Width):F2} пог. м"
            : "—";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
    }
}
