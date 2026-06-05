using System;
using System.IO;
using System.ComponentModel;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace plug.Services
{
    /// <summary>
    /// Паттерн Caretaker (Опекун) из Memento — отвечает за сохранение
    /// и восстановление снимков состояния (<see cref="CalculatorSettingsMemento"/>).<br/>
    /// <br/>
    /// Как работает:<br/>
    /// 1) Подписывается на PropertyChanged в <see cref="CalculatorSettings"/>.<br/>
    /// 2) При изменении свойства запускает debounce (300 мс) — если за это
    ///    время пришло новое изменение, старая задача отменяется.<br/>
    /// 3) <see cref="SaveAsync"/> сериализует Memento в JSON и записывает в файл.<br/>
    /// 4) <see cref="LoadAsync"/> вызывается при старте — читает файл
    ///    и восстанавливает состояние через RestoreMemento().
    /// </summary>
    public class SettingsCaretaker
    {
        private static SettingsCaretaker? _instance;

        /// <summary>Глобальная точка доступа (Singleton).</summary>
        public static SettingsCaretaker GetInstance() =>
            _instance ??= new SettingsCaretaker(CalculatorSettings.GetInstance());

        private readonly CalculatorSettings _settings;
        private readonly string _savePath;
        private bool _isRestoring;
        private CancellationTokenSource? _debounceCts;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        /// <summary>Путь к файлу настроек.</summary>
        public static string SaveFilePath =>
            Path.Combine(AppContext.BaseDirectory, "calculatorsettings.json");

        private SettingsCaretaker(CalculatorSettings settings)
        {
            _settings = settings;
            _savePath = SaveFilePath;
            _settings.PropertyChanged += OnSettingsChanged;
        }

        private void OnSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_isRestoring) return;
            _ = HandleSettingsChangedAsync();
        }

        private async Task HandleSettingsChangedAsync()
        {
            // Debounce: отменяем предыдущий отложенный вызов
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();
            var token = _debounceCts.Token;

            try
            {
                await Task.Delay(300, token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            await SaveAsync(token);
        }

        /// <summary>
        /// Асинхронно сохранить текущее состояние настроек в файл JSON.
        /// </summary>
        public async Task SaveAsync(CancellationToken ct = default)
        {
            try
            {
                var memento = _settings.CreateMemento();
                var json = JsonSerializer.Serialize(memento, JsonOptions);
                await File.WriteAllTextAsync(_savePath, json, ct);
            }
            catch (OperationCanceledException)
            {
                // Сохранение отменено — пришло новое изменение
            }
        }

        /// <summary>
        /// Загрузить и восстановить состояние из файла JSON (при старте приложения).
        /// </summary>
        public async Task LoadAsync()
        {
            if (!File.Exists(_savePath)) return;

            _isRestoring = true;
            try
            {
                var json = await File.ReadAllTextAsync(_savePath);
                var memento = JsonSerializer.Deserialize<CalculatorSettingsMemento>(json);
                if (memento is not null)
                    _settings.RestoreMemento(memento);
            }
            finally
            {
                _isRestoring = false;
            }
        }
    }
}
