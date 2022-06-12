using MauiAppDemo.Pages;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
public class LoginPageViewModel : ValidationPropertyModel
{
    private int? _No;
    private string _Password;

    /// <summary>
    /// 社員番号
    /// </summary>
    [Display(Order = 1, Name ="社員番号")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [Range(1, 9999, ErrorMessage = "{0}は{1}～{2}の間で入力してください。")]
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
    [Display(Order = 2, Name = "パスワード")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [MinLength(1, ErrorMessage = "{0}は必須です。")]
    public string Password
    {
        set
        {
            _Password = value;
            OnPropertyChanged();
        }
        get => _Password;
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
                    AddModelError("ログインできませんでした。社員番号とパスワードを確認してください。");
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
