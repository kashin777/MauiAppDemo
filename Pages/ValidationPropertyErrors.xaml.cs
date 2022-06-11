using System.ComponentModel;

namespace MauiAppDemo.Pages;

public partial class ValidationPropertyErrors : ContentView
{
    public static readonly BindableProperty TargetModelProperty = 
        BindableProperty.Create("TargetModel", typeof(ValidationPropertyModel), typeof(ValidationPropertyErrors), propertyChanged: TargetModelProperty_PropertyChanged);

    public ValidationPropertyModel TargetModel
    {
        set => SetValue(TargetModelProperty, value);
        get => GetValue(TargetModelProperty) as ValidationPropertyModel;
    }

    public ValidationPropertyErrors()
	{
		InitializeComponent();
	}

    static void TargetModelProperty_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorView = (ValidationPropertyErrors)bindable;

        if (oldValue != null)
        {
            errorView.TargetModel.PropertyChanged -= errorView.TargetModel_PropertyChanged;
        }

        if (newValue != null)
        {
            errorView.TargetModel.PropertyChanged += errorView.TargetModel_PropertyChanged;
        }
    }

    private void TargetModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Errors"))
        {
            var errorViewModel = BindingContext as ValidationPropertyErrorsViewModel;
            errorViewModel.ClearErrors();
            foreach (var error in TargetModel.Errors)
            {
                errorViewModel.AddError(ValidationPropertyModel.MODEL_ERROR, error);
            }
        }
    }
}

public class ValidationPropertyErrorsViewModel : ValidationPropertyModel
{

}