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
        #region
        [ObservableProperty]
        CommitmentsView commitmentsView = new();

        [ObservableProperty]
        DailyView dailyView = new();

        [ObservableProperty]
        FinanceView financeView = new();

        #endregion
        public ShellVM()
        {
            commitmentsView.commitmentsVM = new() { ShellVM = this };
            dailyView.dailyVM = new() { ShellVM = this };
        }
    }
}
