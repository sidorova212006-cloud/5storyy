using plug.Services;
using Xunit;

namespace plug.Tests
{
    /// <summary>
    /// Тесты паттернов Singleton и Originator — <see cref="CalculatorSettings"/>.
    /// </summary>
    public class CalculatorSettingsTests
    {
        // GetInstance не возвращает null
        [Fact]
        public void GetInstance_ReturnsNonNull()
        {
            Assert.NotNull(CalculatorSettings.GetInstance());
        }

        // Повторный вызов GetInstance возвращает тот же экземпляр (Singleton)
        [Fact]
        public void GetInstance_ReturnsSameInstance()
        {
            Assert.Same(CalculatorSettings.GetInstance(), CalculatorSettings.GetInstance());
        }

        // Precision можно изменить и прочитать обратно
        [Fact]
        public void Precision_CanBeSet()
        {
            var s = CalculatorSettings.GetInstance();
            var original = s.Precision;
            try
            {
                s.Precision = 4;
                Assert.Equal(4, s.Precision);
            }
            finally
            {
                s.Precision = original;
            }
        }

        // Изменение Precision вызывает PropertyChanged с правильным именем
        [Fact]
        public void Precision_RaisesPropertyChanged()
        {
            var s = CalculatorSettings.GetInstance();
            var original = s.Precision;
            try
            {
                var newVal = s.Precision + 1;
                string? changed = null;
                s.PropertyChanged += (_, e) => changed = e.PropertyName;
                s.Precision = newVal;
                Assert.Equal("Precision", changed);
            }
            finally
            {
                s.Precision = original;
            }
        }

        // Установка того же значения НЕ вызывает PropertyChanged
        [Fact]
        public void SameValue_DoesNotRaisePropertyChanged()
        {
            var s = CalculatorSettings.GetInstance();
            s.Precision = 2;
            int count = 0;
            s.PropertyChanged += (_, _) => count++;
            s.Precision = 2;
            Assert.Equal(0, count);
        }

        // CreateMemento сохраняет текущее Precision
        [Fact]
        public void CreateMemento_CapturesPrecision()
        {
            var s = CalculatorSettings.GetInstance();
            var original = s.Precision;
            try
            {
                s.Precision = 3;
                var m = s.CreateMemento();
                Assert.Equal(3, m.Precision);
            }
            finally
            {
                s.Precision = original;
            }
        }

        // CreateMemento делает копию LastResults, а не ссылку
        [Fact]
        public void CreateMemento_CopiesLastResults()
        {
            var s = CalculatorSettings.GetInstance();
            try
            {
                s.ClearHistory();
                s.LastResults.Add("тест");
                var m = s.CreateMemento();
                s.LastResults.Add("ещё");
                Assert.Single(m.LastResults); // снимок не изменился
            }
            finally
            {
                s.ClearHistory();
            }
        }

        // RestoreMemento восстанавливает Precision
        [Fact]
        public void RestoreMemento_RestoresPrecision()
        {
            var s = CalculatorSettings.GetInstance();
            var original = s.Precision;
            try
            {
                s.RestoreMemento(new CalculatorSettingsMemento(5, new List<string>()));
                Assert.Equal(5, s.Precision);
            }
            finally
            {
                s.Precision = original;
            }
        }

        // Create + Restore: данные не искажаются (roundtrip)
        [Fact]
        public void CreateAndRestoreMemento_RoundTrip()
        {
            var s = CalculatorSettings.GetInstance();
            var original = s.Precision;
            try
            {
                s.Precision = 3;
                s.ClearHistory();
                s.LastResults.Add("результат1");
                s.LastResults.Add("результат2");

                var memento = s.CreateMemento();
                
                // Меняем состояние
                s.Precision = 5;
                s.LastResults.Clear();
                
                // Восстанавливаем
                s.RestoreMemento(memento);
                
                Assert.Equal(3, s.Precision);
                Assert.Equal(2, s.LastResults.Count);
                Assert.Equal("результат1", s.LastResults[0]);
                Assert.Equal("результат2", s.LastResults[1]);
            }
            finally
            {
                s.Precision = original;
                s.ClearHistory();
            }
        }
    }
}
