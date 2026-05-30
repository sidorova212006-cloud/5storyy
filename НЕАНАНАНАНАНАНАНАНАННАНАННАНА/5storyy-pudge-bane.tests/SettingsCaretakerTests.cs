using System.IO;
using System.Text.Json;
using plug.Services;
using Xunit;

namespace plug.Tests
{
    public class SettingsCaretakerTests
    {
        private void CleanupSettingsFile()
        {
            if (File.Exists(SettingsCaretaker.SaveFilePath))
                File.Delete(SettingsCaretaker.SaveFilePath);
        }

        [Fact]
        public void SaveFilePath_EndsWithExpectedFileName()
        {
            Assert.EndsWith("calculatorsettings.json", SettingsCaretaker.SaveFilePath);
        }

        [Fact]
        public void GetInstance_ReturnsNonNull()
        {
            Assert.NotNull(SettingsCaretaker.GetInstance());
        }

        [Fact]
        public void GetInstance_ReturnsSameInstance()
        {
            Assert.Same(SettingsCaretaker.GetInstance(), SettingsCaretaker.GetInstance());
        }

        [Fact]
        public async Task SaveAsync_WritesCorrectPrecisionToJson()
        {
            CleanupSettingsFile();
            var settings = CalculatorSettings.GetInstance();
            var originalPrecision = settings.Precision;
            try
            {
                settings.Precision = 3;

                await SettingsCaretaker.GetInstance().SaveAsync();

                Assert.True(File.Exists(SettingsCaretaker.SaveFilePath));
                var json = await File.ReadAllTextAsync(SettingsCaretaker.SaveFilePath);
                var m = JsonSerializer.Deserialize<CalculatorSettingsMemento>(json);
                Assert.NotNull(m);
                Assert.Equal(3, m.Precision);
            }
            finally
            {
                settings.Precision = originalPrecision;
                CleanupSettingsFile();
            }
        }

        [Fact]
        public async Task LoadAsync_RestoresPrecisionFromFile()
        {
            CleanupSettingsFile();
            var settings = CalculatorSettings.GetInstance();
            var originalPrecision = settings.Precision;
            var caretaker = SettingsCaretaker.GetInstance();
            try
            {
                settings.Precision = 4;
                await caretaker.SaveAsync();

                settings.Precision = 0;
                await caretaker.LoadAsync();

                Assert.Equal(4, settings.Precision);
            }
            finally
            {
                settings.Precision = originalPrecision;
                CleanupSettingsFile();
            }
        }

        [Fact]
        public async Task LoadAsync_WhenNoFileExists_DoesNotThrow()
        {
            CleanupSettingsFile();

            await SettingsCaretaker.GetInstance().LoadAsync();
        }

        [Fact]
        public async Task SaveAndLoad_FullRoundTrip_PreservesAllData()
        {
            CleanupSettingsFile();
            var settings = CalculatorSettings.GetInstance();
            var originalPrecision = settings.Precision;
            var caretaker = SettingsCaretaker.GetInstance();
            try
            {
                settings.Precision = 3;
                settings.ClearHistory();
                settings.LastResults.Add("Линолеум: 5.00 п.м.");

                await caretaker.SaveAsync();

                settings.Precision = 0;
                settings.ClearHistory();

                await caretaker.LoadAsync();

                Assert.Equal(3, settings.Precision);
                Assert.Contains("Линолеум: 5.00 п.м.", settings.LastResults);
            }
            finally
            {
                settings.Precision = originalPrecision;
                settings.ClearHistory();
                CleanupSettingsFile();
            }
        }
    }
}
