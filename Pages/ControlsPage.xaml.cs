namespace MauiAppDemo.Pages;

public partial class ControlsPage : ContentPage
{
	public ControlsPage()
	{
		InitializeComponent();

		BindingContext = new ControlsPageViewModel();
	}
}

public class ControlsPageViewModel : ValidationPropertyViewModel
{

}

