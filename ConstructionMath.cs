using System;

public static class ConstructionMath
{
    // 1. Квадратные метры в погонные (для линолеума)
    public static double SqMetToLinear(double area, double width) => area / width;

    // 2. Площадь стен (Периметр * Высота - Площадь проемов)
    public static double WallArea(double length, double width, double height, double openingsArea) 
        => (2 * (length + width) * height) - openingsArea;

    // 3. Объем бетона (плита)
    public static double ConcreteVolumeSlab(double length, double width, double depth) 
        => length * width * depth;

    // 4. Расчет плитки с запасом (10%) и округлением до упаковок
    public static int TilePackages(double area, double m2InPackage)
    {
        double totalWithWaste = area * 1.10; // +10%
        return (int)Math.Ceiling(totalWithWaste / m2InPackage);
    }

    // 5. Длина стропил через угол (Гипотенуза = Катет / cos(заданный угол))
    public static double RafterLength(double horizontalProjection, double angleDegrees)
    {
        double angleRadians = angleDegrees * (Math.PI / 180);
        return horizontalProjection / Math.Cos(angleRadians);
    }
}