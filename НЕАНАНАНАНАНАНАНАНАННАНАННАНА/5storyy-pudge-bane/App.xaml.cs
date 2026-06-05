using System.Windows;
using plug.Services;

namespace plug;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Инициализируем Caretaker (подписывается на PropertyChanged в CalculatorSettings)
        // и восстанавливаем настройки из файла
        _ = InitializeAsync();
    }

    private static async Task InitializeAsync()
    {
        await SettingsCaretaker.GetInstance().LoadAsync();
    }
}
