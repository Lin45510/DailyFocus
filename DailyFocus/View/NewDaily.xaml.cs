using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class NewDaily : ContentView
{
    public NewDailyVM vm;
    public NewDaily()
    {
        InitializeComponent();
        BindingContext = vm;
    }
}