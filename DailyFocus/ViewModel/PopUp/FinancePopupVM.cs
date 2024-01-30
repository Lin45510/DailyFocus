using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using DailyFocus.View.PopUp;

namespace DailyFocus.ViewModel.PopUp
{
    public partial class FinancePopupVM : ObservableObject
    {
        private readonly FinanceModel _model = new();

        #region Observbale Properties

        [ObservableProperty]
        FinanceModel finance;

        [ObservableProperty]
        ShellVM shellVM;

        #endregion

        public FinancePopupVM() { }

        #region Commands

        [RelayCommand]
        async Task Edit(FinancePopup financePopup)
        {
            await _model.EditPopUp(Finance, ShellVM);
            await financePopup.CloseAsync();
        }

        [RelayCommand]
        async Task Delete(FinancePopup financePopup)
        {
            await _model.Delete(Finance);

            await ShellVM.FinanceView.FinnanceVM.LoadFinances();

            await financePopup.CloseAsync();
        }

        #endregion
    }
}
