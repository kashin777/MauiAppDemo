using Microsoft.Maui.Platform;

namespace MauiAppDemo;

public static class MauiProgram
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

		// 
        Microsoft.Maui.Handlers.EntryHandler.Mapper.ModifyMapping(nameof(IEntry.Background), (handler, entry, action) => 
		{
#if WINDOWS

#endif
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Gray.ToPlatform());
#endif
		});

        return builder.Build();
	}
}
