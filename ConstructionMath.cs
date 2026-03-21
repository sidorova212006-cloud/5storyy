using System;

namespace BuildCalc;

public static class ConstructionMath
{
    // Метод для расчета количества упаковок плитки
    public static int TilePackages(double tileArea, double m2InPackage)
    {
        if (m2InPackage <= 0) return 0; // Защита от деления на ноль
        
        // Округляем количество упаковок в большую сторону (Math.Ceiling)
        return (int)Math.Ceiling(tileArea / m2InPackage); 
    }
}