using CommunityToolkit.Maui.Views;
using System.Drawing;

namespace DailyFocus.Resources.Utilities
{
    public class LazyView<TView> : LazyView where TView : ContentView, new()
    {
        public LazyView() { }
        public override async ValueTask LoadViewAsync(CancellationToken token = default)
        {
            StackLayout layout = new() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

            layout.Add(new ActivityIndicator() { IsRunning = true, HorizontalOptions = LayoutOptions.Center });
            layout.Add(new Label() { Text = "Carregando", HorizontalOptions = LayoutOptions.Center });

            Content = layout;
            await Task.Delay(250);
            Content = new TView() { BindingContext = BindingContext };
            SetHasLazyViewLoaded(true);
        }
    }
}
