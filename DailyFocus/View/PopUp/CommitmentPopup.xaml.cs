using CommunityToolkit.Maui.Views;
using DailyFocus.ViewModel;
using DailyFocus.ViewModel.PopUp;

namespace DailyFocus.View.PopUp;

public partial class CommitmentPopup : Popup
{
    public CommitmentPopup(CommitmentPopUpVM vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}