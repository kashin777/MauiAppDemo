namespace MauiAppDemo.Pages;

public partial class SubPage : ContentPage
{
<<<<<<< HEAD
=======


    public int No { set; get; }

>>>>>>> 0ec0ae334bc47da12c7ee1f8667d9f45217fc81a
	public SubPage()
	{
		InitializeComponent();

		BindingContext = new SubPageViewModel();
	}
}

public class SubPageViewModel : ValidationPropertyViewModel
{
<<<<<<< HEAD

=======
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
>>>>>>> 0ec0ae334bc47da12c7ee1f8667d9f45217fc81a
}

