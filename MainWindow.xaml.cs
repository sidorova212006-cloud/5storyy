using System.Windows;

namespace plug;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ConstructionViewModel();
    }
}