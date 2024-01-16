using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Commitments : ContentPage
    {
        public Commitments(CommitmentsVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}