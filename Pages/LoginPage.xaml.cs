using MauiAppDemo.Pages;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MauiAppDemo.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }
}

public class LoginPageViewModel : ValidationPropertyModel
{
    private int? _No;
    private string _Password;

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

    public ICommand LoginCommand { protected set; get; }

    public LoginPageViewModel()
    {
        LoginCommand = new Command(() =>
        {
            if (Validate())
            {
                if (No.ToString().Equals(Password))
                {
                    Shell.Current.GoToAsync($"///MainPage?No={No}");

                    No = null;
                    Password = null;
                    ClearErrors();
                }
                else
                {
                    AddModelError("ログインできませんでした。社員番号とパスワードを確認してください。");
                }
            }
        },
        () =>
        {
            return !HasErrors;
        });
    }
}
