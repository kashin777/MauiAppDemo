using System.ComponentModel;

namespace MauiAppDemo.Pages;

public partial class ValidationPropertyError : ContentView
{
    public static readonly BindableProperty TargetModelProperty = 
        BindableProperty.Create("TargetModel", typeof(ValidationPropertyModel), typeof(ValidationPropertyError), propertyChanged: TargetModelProperty_PropertyChanged);

    public ValidationPropertyModel TargetModel
    {
        set => SetValue(TargetModelProperty, value);
        get => GetValue(TargetModelProperty) as ValidationPropertyModel;
    }

    public string PropertyName
    {
        get;
        set;
    }

    public ValidationPropertyError()
    {
        InitializeComponent();
    }

    static void TargetModelProperty_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorView = (ValidationPropertyError)bindable;

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
            var errorViewModel = BindingContext as ValidationPropertyErrorViewModel;
            errorViewModel.ClearErrors();
            foreach (var error in TargetModel.PropertyErrors(PropertyName))
            {
                errorViewModel.AddError(PropertyName, error);
            }
        }
    }
}

public class ValidationPropertyErrorViewModel : ValidationPropertyModel
{

}