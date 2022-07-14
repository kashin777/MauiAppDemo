using Microsoft.Maui.Platform;
using MauiAppDemo.Pages;
using MauiAppDemo.Services;

namespace MauiAppDemo;

public static partial class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Depedency Injection
        builder.Services
			.AddTransient<LoginPage>()
			.AddTransient<LoginPageViewModel>()
			.AddTransient<MainPage>()
			.AddTransient<MainPageViewModel>()
			.AddSingleton<IMauiAppDemoService, MauiAppDemoService>();

        // プラットフォームに応じたカスタマイズ
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.ModifyMapping(nameof(IDatePicker.Background), (handler, picker, action) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
        });
        Microsoft.Maui.Handlers.EditorHandler.Mapper.ModifyMapping(nameof(IEditor.Background), (handler, editor, action) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
        });
        Microsoft.Maui.Handlers.EntryHandler.Mapper.ModifyMapping(nameof(IEntry.Background), (handler, entry, action) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
        });
        Microsoft.Maui.Handlers.PickerHandler.Mapper.ModifyMapping(nameof(IPicker.Background), (handler, picker, action) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
        });
        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.ModifyMapping(nameof(ITimePicker.Background), (handler, picker, action) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
        });


        return builder.Build();
	}
}
