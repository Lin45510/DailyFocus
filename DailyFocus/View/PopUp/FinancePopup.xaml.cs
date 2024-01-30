using CommunityToolkit.Maui.Views;
using DailyFocus.ViewModel.PopUp;

namespace DailyFocus.View.PopUp;

public partial class FinancePopup : Popup
{
    public FinancePopup(FinancePopupVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}