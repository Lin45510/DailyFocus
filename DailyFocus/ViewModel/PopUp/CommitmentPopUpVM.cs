using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.DataBase.DAO;
using DailyFocus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.ViewModel.PopUp
{
    public partial class CommitmentPopUpVM : ObservableObject
    {
        private readonly CommitmentsModel _model = new();

        #region Observable Property
        [ObservableProperty]
        ShellVM shellVM;

        [ObservableProperty]
        CommitmentsModel commitment;

        #endregion

        public CommitmentPopUpVM()
        {
        }

        [RelayCommand]
        async Task Delete(Popup popup)
        {
            await _model.Delete(Commitment);
            ShellVM.CommitmentsView.commitmentsVM.Commitments = await _model.GroupCommitmentsbyDate();
            ShellVM.DailyView.dailyVM.Commitments = await _model.GroupCommitmentsbyDateTime();
            popup.Close();
        }
        [RelayCommand]
        async Task Edit(Popup popup)
        {
            await _model.EditPopUp(Commitment, ShellVM);
            popup.Close();
        }
    }
}
