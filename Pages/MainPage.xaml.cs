using MauiAppDemo.Models;
using MauiAppDemo.Pages;
using MauiAppDemo.Services;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using static MauiAppDemo.MauiProgram;

namespace MauiAppDemo.Pages;

/// <summary>
/// メインページ。
/// </summary>
public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;

	}
}

/// <summary>
/// メインページのビューモデル。
/// </summary>
public class MainPageViewModel : ValidationPropertyViewModel
{

    private User _LoginUser;

    public User LoginUser { 
        get => _LoginUser;
        set => SetProperty(ref _LoginUser, value); 
    }

    /// <summary>
    /// ログアウトコマンド
    /// </summary>
    public ICommand LogoutCommand { protected set; get; }

    public MainPageViewModel(IMauiAppDemoService service)
    {
        LoginUser = service.LoginUser;

        // ログアウトコマンドの実装
        LogoutCommand = new Command(async() =>
        {
            LoginUser = null;

            await Shell.Current.GoToAsync("///LoginPage");
        },
        () =>
        {
            return true;
        });
    }
}

