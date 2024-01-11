using CommunityToolkit.Maui;
using DailyFocus.View;
using DailyFocus.ViewModel;
using Microsoft.Extensions.Logging;

namespace DailyFocus
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Inter.ttc", "Inter");
            }).UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<Commitments>();
            builder.Services.AddSingleton<Daily>();
            builder.Services.AddSingleton<Finance>();

            builder.Services.AddTransient<NewCommitment>();
            builder.Services.AddTransient<NewCommitmentVM>();

            builder.Services.AddTransient<NewDaily>();
            builder.Services.AddTransient<NewDailyVM>();

            return builder.Build();
        }
    }
}