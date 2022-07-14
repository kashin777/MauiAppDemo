namespace MauiAppDemo.Pages;

public partial class ControlsPage : ContentPage
{
	public ControlsPage()
	{
		InitializeComponent();

		BindingContext = new ControlsPageViewModel();
	}

	private void ProgressBar_Animation(object sender, EventArgs e)
	{
		ProgressBar1.Progress = 0;
		ProgressBar1.ProgressTo(1.0, 3000, Easing.Linear);
        ProgressBar2.Progress = 0;
        ProgressBar2.ProgressTo(1.0, 3000, Easing.SpringIn);
        ProgressBar3.Progress = 0;
        ProgressBar3.ProgressTo(1.0, 3000, Easing.CubicIn);
        ProgressBar4.Progress = 0;
        ProgressBar4.ProgressTo(1.0, 3000, Easing.BounceIn);
        ProgressBar5.Progress = 0;
        ProgressBar5.ProgressTo(1.0, 3000, Easing.BounceOut);

    }
}

public class ControlsPageViewModel : ValidationPropertyViewModel
{

}

