using DailyFocus.Model;
using DailyFocus.Resources.Utilities;
using DailyFocus.ViewModel;

namespace DailyFocus.View.LazyView;

public partial class CommitmentsView : ContentPage
{
    public CommitmentsVM commitmentsVM { get; set; }
    public CommitmentsView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        lazy.BindingContext = commitmentsVM;
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