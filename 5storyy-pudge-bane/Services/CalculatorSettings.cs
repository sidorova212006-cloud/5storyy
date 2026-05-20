using System.Collections.Generic;

namespace plug.Services
{
    /// <summary>
    /// Singleton. Хранит глобальные настройки калькулятора (точность вывода, история результатов).
    /// Не потокобезопасен — рассчитан на UI-поток WPF.
    /// </summary>
    public sealed class CalculatorSettings
    {
        private static CalculatorSettings? _instance;

        private CalculatorSettings()
        {
            Precision = 2;
            LastResults = new List<string>();
        }

        public int Precision { get; set; }

        public List<string> LastResults { get; }

        public static CalculatorSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CalculatorSettings();
            }

            return _instance;
        }

        public void ClearHistory()
        {
            LastResults.Clear();
        }
    }
}
