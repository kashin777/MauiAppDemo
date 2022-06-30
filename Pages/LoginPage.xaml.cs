using MauiAppDemo.Pages;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MauiAppDemo.Pages;

/// <summary>
/// ログインページ。
/// </summary>
public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
    }
}

/// <summary>
/// ログインページのビューモデル。
/// </summary>
public class LoginPageViewModel : ValidationPropertyViewModel
{
    private StringKeyValuPair _Language = (App.Current as App).Language;
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

    public LoginPageViewModel()
    {
        // 言語変更コマンドの実装
        LanguageCommand = new Command(() => 
        {
            var app = (App.Current as App);
            app.Language = Language;
            app.SwichLanguage();
        }, 
        () => { 
            return !Language.Equals((App.Current as App).Language); 
        });

        // ログインコマンドの実装
        LoginCommand = new Command(() =>
        {
            // データ検証
            if (Validate())
            {
                // 社員番号とパスワードが一致するか？
                if (No.ToString().Equals(Password))
                {
                    // メインページへ移動 (社員番号をQueryParameterとして渡す。)
                    Shell.Current.GoToAsync($"///MainPage?No={No}");

                    // 初期化
                    No = null;
                    Password = null;
                    ClearErrors();
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
