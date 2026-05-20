using System;

namespace plug.Services
{
    public static class ConstructionMath
    {
        public static double LinearMeters(double area, double width)
        {
            return width <= 0 ? 0 : area / width;
        }

        public static double WallArea(double length, double width, double height, double openingsArea)
        {
            var totalWallArea = (length + width) * 2 * height;
            return Math.Max(0, totalWallArea - openingsArea);
        }

        public static double ConcreteVolume(double length, double width, double depth)
        {
            return Math.Max(0, length * width * depth);
        }

        public static double TilePackages(double area, double m2InPackage)
        {
            return m2InPackage <= 0 ? 0 : Math.Ceiling(area * 1.1 / m2InPackage);
        }


        public static double RafterLength(double projection, double angleDegrees)
        {
            if (projection <= 0 || angleDegrees <= 0 || angleDegrees >= 90)
            {
                return 0;
            }

            var radians = angleDegrees * Math.PI / 180.0;
            return projection / Math.Cos(radians);
        }
    }

}
