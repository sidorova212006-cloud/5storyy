using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace plug.Services
{
    /// <summary>
    /// Паттерн Memento (Хранитель) — неизменяемый снимок состояния
    /// настроек калькулятора в определённый момент времени.<br/>
    /// Сериализуется в JSON файл через <see cref="SettingsCaretaker"/>.<br/>
    /// Объект неизменяем (record с init-свойствами), поэтому
    /// сохранённое состояние невозможно случайно повредить.
    /// </summary>
    public record CalculatorSettingsMemento(
        [property: JsonPropertyName("precision")] int Precision,
        [property: JsonPropertyName("lastResults")] List<string> LastResults
    );
}
