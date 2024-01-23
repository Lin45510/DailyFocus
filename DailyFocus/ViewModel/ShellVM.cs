using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DailyFocus.View.LazyView;

namespace DailyFocus.ViewModel
{
    public partial class ShellVM : ObservableObject
    {
        #region Observable Properties

        [ObservableProperty]
        CommitmentsView commitmentsView = new();

        [ObservableProperty]
        DailyView dailyView = new();

        [ObservableProperty]
        FinanceView financeView = new();

        [ObservableProperty]
        NewDailyView newDailyView = new();

        #endregion
        public ShellVM()
        {
            commitmentsView.CommitmentsVM = new() { ShellVM = this };
            dailyView.DailyVM = new() { ShellVM = this };
            financeView.FinnanceVM = new() { ShellVM = this };
            newDailyView.NewDailyVM = new() { ShellVM = this };
        }
    }
}
