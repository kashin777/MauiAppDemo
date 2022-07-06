using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Input;
using MauiAppDemo.Models;
using MauiAppDemo.Utils;
using MauiAppDemo.Services;

namespace MauiAppDemo.Pages;

/// <summary>
/// ログインページ。
/// </summary>
public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
	{
        // 言語を変更
        var culture = new CultureInfo(Preferences.Get("Language", "ja"));
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        InitializeComponent();

        BindingContext = viewModel;
    }
}

/// <summary>
/// ログインページのビューモデル。
/// </summary>
public class LoginPageViewModel : ValidationPropertyViewModel
{
    private StringKeyValuPair _Language = new StringKeyValuPair() { Key = Preferences.Get("Language", "ja") };
    private int? _No;
    private string _Password;

    /// <summary>
    /// 社員番号
    /// </summary>
    [Display(Order = 1, Name = nameof(Messages.Login_No), ResourceType = typeof(Messages))]
    [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
    [Range(1, 9999, ErrorMessageResourceName = nameof(Messages.Error_Range), ErrorMessageResourceType = typeof(Messages))]
    public int? No {
        set
        {
            SetProperty(ref _No, value);
            ChangeCanExecute(LoginCommand);
        }
        get => _No;	
    }

    /// <summary>
    /// パスワード
    /// </summary>
    [Display(Order = 2, Name = nameof(Messages.Login_Password), ResourceType = typeof(Messages))]
    [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
    [MinLength(1, ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
    public string Password
    {
        set
        {
            SetProperty(ref _Password, value);
            ChangeCanExecute(LoginCommand);
        }
        get => _Password;
    }

    /// <summary>
    /// 言語
    /// </summary>
    public StringKeyValuPair Language {
        set { 
            SetProperty(ref _Language, value);
            ChangeCanExecute(LanguageCommand);
        }
        get => _Language;
    }

    /// <summary>
    /// 言語変更コマンド。
    /// </summary>
    public ICommand LanguageCommand { protected set; get; }

    /// <summary>
    /// ログインコマンド。
    /// </summary>
    public ICommand LoginCommand { protected set; get; }

    public LoginPageViewModel(IMauiAppDemoService service)
    {
        // 言語変更コマンドの実装
        LanguageCommand = new Command(() => 
        {
            // 言語を保存
            Preferences.Set("Language", Language.Key);

            // 画面を初期化
            App.Current.MainPage = new AppShell();
        }, 
        () => { 
            return !Language.Key.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName); 
        });

        // ログインコマンドの実装
        LoginCommand = new Command(async() =>
        {
            // データ検証
            if (Validate())
            {
                var ctx = service.DBContext;

                // 社員番号とパスワードが一致するか？
                var user = await ctx.FindAsync<User>(No);
                if (user != null && user.Password.Equals(Hashs.Sha256(user.No.ToString(), Password)))
                {
                    Shell.Current.CurrentPage.Focus();
                    No = null;
                    Password = null;
                    ClearErrors();

                    // ログインユーザを通知
                    MessagingCenter.Send<User>(user, "Login");

                    // メインページへ移動
                    await Shell.Current.GoToAsync($"///MainPage");

                }
                // 一致しない場合
                else
                {
                    AddModelError(Messages.Error_Comman_Login);
                }
            }

            ChangeCanExecute(LoginCommand);
        },
        () =>
        {
            // 画面にエラーが出ている場合は無効
            return !HasErrors;
        });

        // コマンドを登録
        AddCommands(LanguageCommand, LoginCommand);
    }
}
