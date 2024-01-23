using DailyFocus.ViewModel;

namespace DailyFocus.View.LazyView;

public partial class DailyView : ContentPage
{
    public DailyVM DailyVM { get; set; }
    public DailyView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        lazy.BindingContext = DailyVM;
        base.OnAppearing();
        Task.Run(
            async () =>
            {
                await Task.Delay(50);
                await MainThread.InvokeOnMainThreadAsync(async () =>
                await lazy.LoadViewAsync());
            });
    }
}