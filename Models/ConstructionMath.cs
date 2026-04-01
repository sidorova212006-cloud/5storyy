using System;
using System.Collections.Generic;

namespace plug.Models
{
    public class ConstructionModel
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double OpeningsArea { get; set; }

        public double Depth { get; set; }
        public double HorizontalProjection { get; set; }
        public double AngleDegrees { get; set; }

        public double M2InPackage { get; set; }

        public List<string> MaterialNames { get; set; } = new List<string>();
    }

    public static class ConstructionMath
    {
        /// <summary>
        /// Переводит площадь в погонные метры
        /// </summary>
        public static double SqMetToLinear(double area, double width)
        {
            if (width <= 0) return 0;
            return area / width;
        }

        /// <summary>
        /// Площадь стен за вычетом проёмов
        /// </summary>
        public static double WallArea(double length, double width, double height, double openingsArea)
        {
            if (length <= 0 || width <= 0 || height <= 0) return 0;
            double perimeter = 2 * (length + width);
            double grossArea = perimeter * height;
            return Math.Max(0, grossArea - openingsArea);
        }

        /// <summary>
        /// Объём бетона для плиты
        /// </summary>
        public static double ConcreteVolumeSlab(double length, double width, double depth)
        {
            if (length <= 0 || width <= 0 || depth <= 0) return 0;
            return length * width * depth;
        }

        /// <summary>
        /// Количество упаковок плитки с запасом 10%
        /// </summary>
        public static int TilePackages(double area, double m2InPackage)
        {
            if (m2InPackage <= 0 || area <= 0) return 0;
            double totalWithReserve = area * 1.1;
            return (int)Math.Ceiling(totalWithReserve / m2InPackage);
        }

        /// <summary>
        /// Длина стропила по горизонтальной проекции и углу наклона
        /// </summary>
        public static double RafterLength(double projection, double angleDegrees)
        {
            if (projection <= 0 || angleDegrees <= 0 || angleDegrees >= 90) return 0;
            double angleRadians = angleDegrees * Math.PI / 180;
            return projection / Math.Cos(angleRadians);
        }
    }
}