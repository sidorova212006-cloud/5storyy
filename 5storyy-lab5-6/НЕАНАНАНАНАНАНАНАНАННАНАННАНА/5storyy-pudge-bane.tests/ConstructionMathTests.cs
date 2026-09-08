using plug.Services;
using Xunit;

namespace plug.Tests
{
    /// <summary>
    /// Тесты чистых расчётных функций <see cref="ConstructionMath"/>.
    /// Не требуют WPF-окружения — тестируются напрямую.
    /// </summary>
    public class ConstructionMathTests
    {
        // LinearMeters: стандартный расчёт (area / width)
        [Fact]
        public void LinearMeters_Standard_DividesAreaByWidth()
        {
            Assert.Equal(5.0, ConstructionMath.LinearMeters(10.0, 2.0));
        }

        // LinearMeters: нулевая ширина → 0 (защита от деления на ноль)
        [Fact]
        public void LinearMeters_ZeroWidth_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.LinearMeters(10.0, 0));
        }

        // WallArea: вычитает площадь проёмов из общей площади стен
        [Fact]
        public void WallArea_SubtractsOpeningsFromTotal()
        {
            // (4+3)*2*3 - 2 = 42 - 2 = 40
            Assert.Equal(40.0, ConstructionMath.WallArea(4, 3, 3, 2));
        }

        // WallArea: проёмы больше стен → возвращает 0, не отрицательное
        [Fact]
        public void WallArea_OpeningsExceedTotal_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.WallArea(1, 1, 1, 999));
        }

        // ConcreteVolume: перемножает три размера
        [Fact]
        public void ConcreteVolume_MultipliesThreeDimensions()
        {
            Assert.Equal(6.0, ConstructionMath.ConcreteVolume(1, 2, 3));
        }

        // ConcreteVolume: отрицательная глубина → 0
        [Fact]
        public void ConcreteVolume_NegativeDepth_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.ConcreteVolume(5, 5, -1));
        }

        // TilePackages: округляет вверх и добавляет 10% запас
        [Fact]
        public void TilePackages_AddsMarginAndCeilsResult()
        {
            // 10 * 1.1 / 3 = 3.666... → ceil = 4
            Assert.Equal(4, ConstructionMath.TilePackages(10, 3));
        }

        // TilePackages: нулевой размер упаковки → 0
        [Fact]
        public void TilePackages_ZeroPackageSize_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.TilePackages(10, 0));
        }

        // RafterLength: угол 45° → длина = проекция * sqrt(2)
        [Fact]
        public void RafterLength_45Degrees_ReturnsProjectionTimesSqrt2()
        {
            var expected = 10.0 / Math.Cos(Math.PI / 4);
            Assert.Equal(expected, ConstructionMath.RafterLength(10, 45), precision: 10);
        }

        // RafterLength: нулевой угол → 0
        [Fact]
        public void RafterLength_ZeroAngle_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.RafterLength(5, 0));
        }

        // RafterLength: угол >= 90° → 0 (некорректный ввод)
        [Fact]
        public void RafterLength_AngleAtOrAbove90_ReturnsZero()
        {
            Assert.Equal(0, ConstructionMath.RafterLength(5, 90));
            Assert.Equal(0, ConstructionMath.RafterLength(5, 91));
        }
    }
}
