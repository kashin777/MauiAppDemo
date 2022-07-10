namespace MauiAppDemo.Pages;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();

		BindingContext = new SubPageViewModel();
	}
}

public class SubPageViewModel : ValidationPropertyViewModel
{
}

