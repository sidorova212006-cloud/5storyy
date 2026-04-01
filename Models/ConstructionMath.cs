namespace plug.Models
{
    public class ConstructionMath
    {

    }

    public class LinoleumCalculation
    {
        public double Area { get; set; }
        public double Width { get; set; }
    }

    public class WallCalculation
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double OpeningsArea { get; set; }
    }

    public class ConcreteCalculation
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
    }

    public class TileCalculation
    {
        public double Area { get; set; }
        public double M2InPackage { get; set; }
    }

    public class RafterCalculation
    {
        public double Projection { get; set; }
        public double AngleDegrees { get; set; }
    }
}

