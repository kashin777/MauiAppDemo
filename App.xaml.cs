using System.Globalization;

namespace MauiAppDemo;

public partial class App : Application
{
    private int _Language = 0;

    /// <summary>
    /// 現在の言語
    /// </summary>
    public int Language { 
        get 
        {
            return _Language; 
        }
        set 
        {
            _Language = value;
        }
    }

    public App()
	{
        InitializeComponent();

        // メインページを言語に応じて表示
        //MainPage = new AppShell();
        SwichLanguage();

	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Title = Pages.Messages.App_Title;

        return window;
    }

    /// <summary>
    /// 画面の言語を切り替える
    /// </summary>
    public void SwichLanguage()
    {
        var culture = new CultureInfo(Languages[Language].Value);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        (App.Current as App).Language = Language;
        var shell = new AppShell();
        App.Current.MainPage = shell;
    }

    /// <summary>
    /// 言語一覧
    /// </summary>
    public List<KeyValuePair<string, string>> Languages
    {
        get
        {
            var languages = new Dictionary<string, string>();

            languages.Add("日本語", "ja");
            languages.Add("English", "en");

            return languages.ToList();
        }
    }
}
