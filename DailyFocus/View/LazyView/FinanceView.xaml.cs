using DailyFocus.ViewModel;

namespace DailyFocus.View.LazyView;

public partial class FinanceView : ContentPage
{
    public FinnanceVM FinnanceVM { get; set; }
    public FinanceView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        lazy.BindingContext = FinnanceVM;
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