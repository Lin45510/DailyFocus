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

        [ObservableProperty]
        NewDailyVM newDailyVM = null;

        #endregion

        public CommitmentPopUpVM()
        {
        }

        [RelayCommand]
        async Task Delete(Popup popup)
        {
            await _model.Delete(Commitment);
            ShellVM.CommitmentsView.CommitmentsVM.Commitments = await _model.GroupCommitmentsbyDate();
            ShellVM.DailyView.DailyVM.Commitments = await _model.GroupCommitmentsbyDateTime();

            ShellVM.NewDailyView.NewDailyVM.SelectedDate = DateTime.Now.AddDays(1);

            if (NewDailyVM != null)
            {
                NewDailyVM.Commitments = await _model.GetCommitmentsOnDate(Commitment.Date);
                NewDailyVM.NoTimeCommitments = await _model.GetCommitmentsOnDate(Commitment.Date, false);
            }
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
