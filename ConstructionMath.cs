using System;

namespace BuildCalc;

public static class ConstructionMath
{
    public static int TilePackages(double tileArea, double m2InPackage)
    {
        if (m2InPackage <= 0) return 0;
        
        return (int)Math.Ceiling(tileArea / m2InPackage); 
    }
}