using DailyFocus.ViewModel;
using System;

namespace DailyFocus.View.LazyView;

public partial class NewDailyView : ContentPage
{
    public NewDailyVM NewDailyVM { get; set; }
    public NewDailyView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        lazy.BindingContext = NewDailyVM;
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