using Microsoft.Extensions.Logging;
using NewsApp.Services;
using NewsApp.ViewModels;
using NewsApp.Views;

namespace NewsApp
{
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


           // Servicios
        builder.Services.AddSingleton<INewsService, NewsService>();

            // ViewModels
            builder.Services.AddTransient<LatestNewsViewModel>();
            builder.Services.AddTransient<FavoritesViewModel>();

            // Views
            builder.Services.AddTransient<LatestNewsPage>();
            builder.Services.AddTransient<FavoritesPage>();
            builder.Services.AddTransient<NewsDetailPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
