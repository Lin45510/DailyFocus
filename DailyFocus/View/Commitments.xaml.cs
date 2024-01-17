using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Commitments : ContentView
    {
        public Commitments()
        {
            InitializeComponent();
            BindingContext = new CommitmentsVM();
        }
    }
}