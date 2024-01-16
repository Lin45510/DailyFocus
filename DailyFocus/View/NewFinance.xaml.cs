using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class NewFinance : ContentPage
{
    public NewFinance(NewFinanceVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}