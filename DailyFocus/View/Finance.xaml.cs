using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class Finance : ContentPage
{
    public Finance()
    {
        InitializeComponent();
        BindingContext = new FinnanceVM();
    }
}