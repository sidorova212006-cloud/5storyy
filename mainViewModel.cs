using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BuildCalc;

public class ConstructionViewModel : INotifyPropertyChanged
{
    private double _tileArea;
    private double _m2InPackage = 1.5; 

    public double TileArea 
    { 
        get => _tileArea; 
        set 
        { 
            _tileArea = value; 
            OnPropertyChanged(); 
            OnPropertyChanged(nameof(TotalPackages)); 
        } 
    }

    public double M2InPackage 
    { 
        get => _m2InPackage; 
        set 
        { 
            _m2InPackage = value; 
            OnPropertyChanged(); 
            OnPropertyChanged(nameof(TotalPackages)); 
        } 
    }

    public int TotalPackages => ConstructionMath.TilePackages(_tileArea, _m2InPackage);

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? name = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}