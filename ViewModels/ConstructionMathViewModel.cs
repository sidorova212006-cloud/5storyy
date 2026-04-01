using System;
using plug.Models;

namespace plug.ViewModels
{
    public static class ConstructionMath
    {
        public static double SqMetToLinear(double area, double width)
            => width > 0 ? area / width : 0;

        public static double WallArea(double length, double width, double height, double openings)
            => 2 * (length + width) * height - openings;

        public static double ConcreteVolumeSlab(double length, double width, double depth)
            => length * width * depth;

        public static int TilePackages(double area, double m2InPackage)
            => m2InPackage > 0 ? (int)Math.Ceiling(area * 1.1 / m2InPackage) : 0;

        public static double RafterLength(double projection, double angleDeg)
            => (angleDeg > 0 && angleDeg < 90) ? projection / Math.Cos(angleDeg * Math.PI / 180.0) : 0;
    }
}


