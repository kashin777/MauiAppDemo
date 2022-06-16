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
public class MainPageViewModel : ValidationPropertyViewModel
{
    private int _No;

    /// <summary>
    /// 社員番号
    /// </summary>
    [Display(Order = 1, Name = nameof(Messages.Login_No), ResourceType = typeof(Messages))]
    [Required(ErrorMessageResourceName = nameof(Messages.Error_Required), ErrorMessageResourceType = typeof(Messages))]
    [Range(1, 9999, ErrorMessageResourceName = nameof(Messages.Error_Range), ErrorMessageResourceType = typeof(Messages))]
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

