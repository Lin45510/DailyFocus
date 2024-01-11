using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Commitments : ContentPage
    {
        public Commitments()
        {
            InitializeComponent();
            BindingContext = new CommitmentsVM();
        }
    }
}