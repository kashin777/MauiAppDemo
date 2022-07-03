using MauiAppDemo.Pages;
using System.Globalization;

namespace MauiAppDemo;

public partial class App : Application
{

    public App()
	{
        InitializeComponent();

        MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Title = Pages.Messages.App_Title;

        return window;
    }
}
