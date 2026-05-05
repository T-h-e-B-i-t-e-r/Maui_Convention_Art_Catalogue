using CommunityToolkit.Maui;
using MauiApp1.ViewModels;
using MauiApp1.Views;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransientPopup<ArtItemDisplayPopup, ArtItemDisplayPopupViewModel>();
        builder.Services.AddTransientPopup<FavoritedItemsCollectionViewPopup, FavoritedItemsCollectionViewPopupViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}