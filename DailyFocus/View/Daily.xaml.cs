using DailyFocus.Model;
using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Daily : ContentPage
    {
        private readonly CommitmentsModel _commits = new();
        public Daily()
        {
            InitializeComponent();
            BindingContext = new DailyVM();
        }
    }
}

