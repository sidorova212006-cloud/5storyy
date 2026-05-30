using System.Collections.Generic;
using System.ComponentModel;

namespace plug.Services
{
    /// <summary>
    /// Паттерн Singleton (Одиночка) — гарантирует единственный экземпляр
    /// настроек на всё приложение.<br/>
    /// Паттерн Originator (Создатель) из Memento — умеет создавать снимок
    /// состояния (<see cref="CreateMemento"/>) и восстанавливаться из него
    /// (<see cref="RestoreMemento"/>).<br/>
    /// Реализует <see cref="INotifyPropertyChanged"/> (Observer), чтобы
    /// <see cref="SettingsCaretaker"/> автоматически сохранял изменения в файл.
    /// </summary>
    public sealed class CalculatorSettings : INotifyPropertyChanged
    {
        private static CalculatorSettings? _instance;

        private int _precision;

        private CalculatorSettings()
        {
            _precision = 2;
            LastResults = new List<string>();
        }

        /// <summary>Глобальная точка доступа (Singleton).</summary>
        public static CalculatorSettings GetInstance()
        {
            if (_instance == null)
                _instance = new CalculatorSettings();
            return _instance;
        }

        /// <summary>Количество знаков после запятой в результатах.</summary>
        public int Precision
        {
            get => _precision;
            set
            {
                if (_precision != value)
                {
                    _precision = value;
                    OnPropertyChanged(nameof(Precision));
                }
            }
        }

        /// <summary>История результатов вычислений.</summary>
        public List<string> LastResults { get; }

        /// <summary>Очистить историю.</summary>
        public void ClearHistory()
        {
            LastResults.Clear();
            OnPropertyChanged(nameof(LastResults));
        }

        /// <summary>
        /// Создать снимок текущего состояния (Memento).
        /// </summary>
        public CalculatorSettingsMemento CreateMemento() =>
            new(Precision, new List<string>(LastResults));

        /// <summary>
        /// Добавить запись в историю результатов и уведомить слушателей.
        /// </summary>
        public void AddResult(string result)
        {
            LastResults.Add(result);
            OnPropertyChanged(nameof(LastResults));
        }

        /// <summary>
        /// Восстановить состояние из снимка (Memento).
        /// </summary>
        public void RestoreMemento(CalculatorSettingsMemento memento)
        {
            Precision = memento.Precision;
            LastResults.Clear();
            foreach (var r in memento.LastResults)
                LastResults.Add(r);
            OnPropertyChanged(nameof(LastResults));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
