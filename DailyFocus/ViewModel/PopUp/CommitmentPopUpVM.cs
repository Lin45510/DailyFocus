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
        private readonly CommitmentsModel _model;

        #region Observable Property
        [ObservableProperty]
        CommitmentsVM commitmentsVM;

        [ObservableProperty]
        CommitmentsModel commitment;

        #endregion

        public CommitmentPopUpVM()
        {
            _model = new();
        }

        [RelayCommand]
        async Task Delete(Popup popup)
        {
            await _model.Delete(Commitment);
            CommitmentsVM.Commitments = await _model.GroupCommitmentsbyDate();
            popup.Close();
        }
        [RelayCommand]
        async Task Edit(Popup popup)
        {
            await _model.EditPopUp(Commitment, CommitmentsVM);
            popup.Close();
        }
    }
}
