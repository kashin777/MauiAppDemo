namespace MauiAppDemo.Pages;

public partial class SubPage : ContentPage
{


    public int No { set; get; }

	public SubPage()
	{
		InitializeComponent();

		BindingContext = new SubPageViewModel();
	}
}

public class SubPageViewModel : ValidationPropertyViewModel
{
	private bool _CheckBoxIsChecked1;
    private bool _CheckBoxIsChecked2;

    public bool CheckBoxIsChecked1
    { 
		set => SetProperty(ref _CheckBoxIsChecked1, value);
		get => _CheckBoxIsChecked1;
    }

    public bool CheckBoxIsChecked2
    {
        set => SetProperty(ref _CheckBoxIsChecked2, value);
        get => _CheckBoxIsChecked2;
    }
}

