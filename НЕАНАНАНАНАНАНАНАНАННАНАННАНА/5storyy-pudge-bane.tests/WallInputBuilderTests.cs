using plug.Models;
using Xunit;

namespace plug.Tests
{
    /// <summary>
    /// Тесты паттерна Builder — <see cref="WallInputBuilder"/>.
    /// </summary>
    public class WallInputBuilderTests
    {
        // Build устанавливает все четыре свойства корректно
        [Fact]
        public void Build_SetsAllFourProperties()
        {
            var input = new WallInputBuilder()
                .SetLength(4)
                .SetWidth(3)
                .SetHeight(2.5)
                .SetOpenings(1)
                .Build();

            Assert.Equal(4, input.Length);
            Assert.Equal(3, input.Width);
            Assert.Equal(2.5, input.Height);
            Assert.Equal(1, input.OpeningsArea);
        }

        // Build без вызова сеттеров → все нули (значения по умолчанию)
        [Fact]
        public void Build_WithoutSetters_AllZero()
        {
            var input = new WallInputBuilder().Build();

            Assert.Equal(0, input.Length);
            Assert.Equal(0, input.Width);
            Assert.Equal(0, input.Height);
            Assert.Equal(0, input.OpeningsArea);
        }

        // Каждый сеттер возвращает тот же объект Builder (Fluent API)
        [Fact]
        public void SetLength_ReturnsSameBuilder()
        {
            var builder = new WallInputBuilder();
            Assert.Same(builder, builder.SetLength(1));
        }

        // Build каждый раз создаёт новый объект WallInput
        [Fact]
        public void Build_CalledTwice_ReturnsDifferentObjects()
        {
            var builder = new WallInputBuilder().SetLength(5);
            var a = builder.Build();
            var b = builder.Build();
            Assert.NotSame(a, b);
        }
    }
}
