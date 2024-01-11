using DailyFocus.ViewModel;

namespace DailyFocus.View;

public partial class NewCommitment : ContentPage
{
    public NewCommitment(NewCommitmentVM vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}