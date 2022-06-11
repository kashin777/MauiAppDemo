using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiAppDemo.Pages;

public partial class ValidationPropertyError : ContentView
{
    public static readonly BindableProperty PageViewModelProperty = BindableProperty.Create("ValidationPropertyModel", typeof(ValidationPropertyModel), typeof(ValidationPropertyError), propertyChanged: PageViewModel_PropertyChanged);

    public ValidationPropertyModel PageViewModel { 
        set => SetValue(PageViewModelProperty, value);
        get => GetValue(PageViewModelProperty) as ValidationPropertyModel;
    }

    public string PropertyName { 
        get; 
        set; 
    }

	public ValidationPropertyError()
	{
		InitializeComponent();
	}

    static void PageViewModel_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorView = (ValidationPropertyError)bindable;

        if(oldValue != null)
        {
            errorView.PageViewModel.PropertyChanged -= errorView.PageViewModel_PropertyChanged;
        }

        if (newValue != null)
        {
            errorView.PageViewModel.PropertyChanged += errorView.PageViewModel_PropertyChanged;
        }
    }

    private void PageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Errors"))
        {
            var errorViewModel = BindingContext as ValidationPropertyErrorViewModel;
            errorViewModel.ClearErrors();
            foreach (var error in PageViewModel.PropertyErrors(PropertyName))
            {
                errorViewModel.AddError(PropertyName, error);
            }
        }
    }
}

public class ValidationPropertyErrorViewModel : ValidationPropertyModel
{

}