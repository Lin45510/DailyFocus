using DailyFocus.Model;
using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Daily : ContentPage
    {
        public Daily(DailyVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}

