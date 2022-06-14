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

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var culture = new CultureInfo(e.SelectedItem.ToString());
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // TODO
    }
}

/// <summary>
/// ログインページのビューモデル。
/// </summary>
public class LoginPageViewModel : ValidationPropertyModel
{
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
            _No = value;
            OnPropertyChanged();
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
            _Password = value;
            OnPropertyChanged();
        }
        get => _Password;
    }

    public List<string> Languages
    {
        get
        {
            return new string[] { "ja", "en" }.ToList();
        }
    }

    /// <summary>
    /// ログインコマンド。
    /// </summary>
    public ICommand LoginCommand { protected set; get; }

    public LoginPageViewModel()
    {

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
        },
        () =>
        {
            // 画面にエラーが出ている場合は無効
            return !HasErrors;
        });
    }
}
