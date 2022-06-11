using MauiAppDemo.Pages;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace MauiAppDemo.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }
}

[QueryProperty(nameof(No), "No")]
public class MainPageViewModel : PageViewModel
{
    private int _No;
 
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

    public ICommand LogoutCommand { protected set; get; }

    public MainPageViewModel()
    {
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

