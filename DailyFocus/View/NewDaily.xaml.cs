using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class NewDaily : ContentPage
{
    public NewDaily(NewDailyVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}