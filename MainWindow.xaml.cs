using System;
using System.Windows;
using System.Windows.Controls;

namespace BuildCalc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateLinoleum_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtArea.Text, out double area) &&
                double.TryParse(txtMatWidth.Text, out double width) &&
                width > 0)
            {
                double meters = area / width;
                txtLinoleumResult.Text = $"{meters:F2} пог. м";
            }
            else
            {
                txtLinoleumResult.Text = "Проверьте данные";
            }
        }

        private void CalculateTile_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtTileArea == null || txtPackageSize == null || txtTotalPackages == null)
                return;

            if (double.TryParse(txtTileArea.Text, out double tileArea) &&
                double.TryParse(txtPackageSize.Text, out double packageSize) &&
                packageSize > 0)
            {
                double withMargin = tileArea * 1.10; // +10%
                int packages = (int)Math.Ceiling(withMargin / packageSize);
                txtTotalPackages.Text = packages.ToString();
            }
            else
            {
                txtTotalPackages.Text = "—";
            }
        }

        private void CalculateConcrete_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtL.Text, out double l) &&
                double.TryParse(txtW.Text, out double w) &&
                double.TryParse(txtD.Text, out double d))
            {
                double volume = l * w * d;
                txtConcreteResult.Text = $"{volume:F3} м³";
            }
            else
            {
                txtConcreteResult.Text = "Проверьте данные";
            }
        }

        private void CalculateRafters_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtProjection.Text, out double projection) &&
                double.TryParse(txtAngle.Text, out double angleDeg) &&
                angleDeg > 0 && angleDeg < 90)
            {
                double angleRad = angleDeg * Math.PI / 180.0;
                double rafterLength = projection / Math.Cos(angleRad);
                txtRaftersResult.Text = $"{rafterLength:F2} м";
            }
            else
            {
                txtRaftersResult.Text = "Проверьте данные";
            }
        }
    }
}
