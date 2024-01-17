using DailyFocus.Model;
using DailyFocus.ViewModel;

namespace DailyFocus.View
{
    public partial class Daily : ContentView
    {
        public Daily()
        {
            InitializeComponent();
            BindingContext = new DailyVM();
        }
    }
}

