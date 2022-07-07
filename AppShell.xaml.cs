using System.Globalization;

namespace MauiAppDemo;

public partial class AppShell : Shell
{
    public AppShell()
    {
        // 言語を変更
        var culture = new CultureInfo(Preferences.Get("Language", "ja"));
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        InitializeComponent();
    }
}
