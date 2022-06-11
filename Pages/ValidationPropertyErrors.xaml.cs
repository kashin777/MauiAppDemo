using System.ComponentModel;

namespace MauiAppDemo.Pages;

public partial class ValidationPropertyErrors : ContentView
{
    public static readonly BindableProperty PageViewModelProperty = BindableProperty.Create("ValidationPropertyModel", typeof(ValidationPropertyModel), typeof(ValidationPropertyErrors), propertyChanged: PageViewModel_PropertyChanged);

    public ValidationPropertyModel PageViewModel
    {
        set => SetValue(PageViewModelProperty, value);
        get => GetValue(PageViewModelProperty) as ValidationPropertyModel;
    }

    public ValidationPropertyErrors()
	{
		InitializeComponent();
	}

    static void PageViewModel_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorView = (ValidationPropertyErrors)bindable;

        if (oldValue != null)
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
            var errorViewModel = BindingContext as ValidationPropertyErrorsViewModel;
            errorViewModel.ClearErrors();
            foreach (var error in PageViewModel.Errors)
            {
                errorViewModel.AddError(ValidationPropertyModel.MODEL_ERROR, error);
            }
        }
    }
}

public class ValidationPropertyErrorsViewModel : ValidationPropertyModel
{

}