using MauiAppDemo.Pages;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace MauiAppDemo.Pages;

/// <summary>
/// メインページ。
/// </summary>
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
}

/// <summary>
/// メインページのビューモデル。
/// 
/// QueryParameter Noは、社員番号
/// </summary>
[QueryProperty(nameof(No), "No")]
public class MainPageViewModel : ValidationPropertyModel
{
    private int _No;

    /// <summary>
    /// 社員番号
    /// </summary>
    [Display(Order = 1, Name = "社員番号")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [Range(1, 9999, ErrorMessage = "{0}は{1}～{2}の間で入力してください。")]
    public int No
    {
        set
        {
            _No = value;
            OnPropertyChanged();
        }
        get => _No;
    }

    /// <summary>
    /// ログアウトコマンド
    /// </summary>
    public ICommand LogoutCommand { protected set; get; }

    public MainPageViewModel()
    {

        // ログアウトコマンドの実装
        LogoutCommand = new Command(() =>
        {
            Shell.Current.GoToAsync("///LoginPage");
        },
        () =>
        {
            return true;
        });
    }
}

