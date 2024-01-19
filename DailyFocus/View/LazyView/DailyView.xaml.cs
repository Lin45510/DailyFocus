using DailyFocus.ViewModel;

namespace DailyFocus.View.LazyView;

public partial class DailyView : ContentPage
{
    public DailyVM dailyVM { get; set; }
    public DailyView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        lazy.BindingContext = dailyVM;
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