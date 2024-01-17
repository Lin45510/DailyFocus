using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class Finance : ContentView
{
    public Finance()
    {
        InitializeComponent();
        BindingContext = new FinnanceVM();
    }
}