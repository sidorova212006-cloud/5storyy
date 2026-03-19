using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ConstructionViewModel : INotifyPropertyChanged
{
    // ===== Линолеум =====
    private double _linoleumArea;
    private double _linoleumWidth = 1.5;
    public double LinoleumArea
    {
        get => _linoleumArea;
        set { _linoleumArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
    }
    public double LinoleumWidth
    {
        get => _linoleumWidth;
        set { _linoleumWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(LinearMeters)); }
    }
    public string LinearMeters => _linoleumWidth > 0
        ? $"{ConstructionMath.SqMetToLinear(_linoleumArea, _linoleumWidth):F2} пог. м"
        : "—";

    // ===== Площадь стен =====
    private double _wallLength, _wallWidth, _wallHeight, _openingsArea;
    public double WallLength
    {
        get => _wallLength;
        set { _wallLength = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
    }
    public double WallWidth
    {
        get => _wallWidth;
        set { _wallWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
    }
    public double WallHeight
    {
        get => _wallHeight;
        set { _wallHeight = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
    }
    public double OpeningsArea
    {
        get => _openingsArea;
        set { _openingsArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(WallAreaResult)); }
    }
    public string WallAreaResult =>
        $"{ConstructionMath.WallArea(_wallLength, _wallWidth, _wallHeight, _openingsArea):F2} м²";

    // ===== Бетон =====
    private double _concreteLength, _concreteWidth, _concreteDepth;
    public double ConcreteLength
    {
        get => _concreteLength;
        set { _concreteLength = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
    }
    public double ConcreteWidth
    {
        get => _concreteWidth;
        set { _concreteWidth = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
    }
    public double ConcreteDepth
    {
        get => _concreteDepth;
        set { _concreteDepth = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConcreteVolume)); }
    }
    public string ConcreteVolume =>
        $"{ConstructionMath.ConcreteVolumeSlab(_concreteLength, _concreteWidth, _concreteDepth):F3} м³";

    // ===== Плитка =====
    private double _tileArea;
    private double _m2InPackage = 1.5;
    public double TileArea
    {
        get => _tileArea;
        set { _tileArea = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
    }
    public double M2InPackage
    {
        get => _m2InPackage;
        set { _m2InPackage = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPackages)); }
    }
    public string TotalPackages => _m2InPackage > 0
        ? $"{ConstructionMath.TilePackages(_tileArea, _m2InPackage)} уп."
        : "—";

    // ===== Стропила =====
    private double _rafterProjection;
    private double _rafterAngle;
    public double RafterProjection
    {
        get => _rafterProjection;
        set { _rafterProjection = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
    }
    public double RafterAngle
    {
        get => _rafterAngle;
        set { _rafterAngle = value; OnPropertyChanged(); OnPropertyChanged(nameof(RafterLength)); }
    }
    public string RafterLength => (_rafterAngle > 0 && _rafterAngle < 90)
        ? $"{ConstructionMath.RafterLength(_rafterProjection, _rafterAngle):F2} м"
        : "—";

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}