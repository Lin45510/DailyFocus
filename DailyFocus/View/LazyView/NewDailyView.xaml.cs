using System;

namespace DailyFocus.View.LazyView;

public partial class NewDailyView : ContentPage
{
    public NewDailyView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
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