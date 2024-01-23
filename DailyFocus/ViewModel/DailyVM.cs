using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using DailyFocus.View;
using DailyFocus.View.LazyView;
using DailyFocus.View.PopUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.ViewModel
{
    public partial class DailyVM : ObservableObject
    {
        private readonly CommitmentsModel _model = new();

        #region Observable Porperties

        [ObservableProperty]
        ObservableCollection<CommitmentsModel> commitments;

        [ObservableProperty]
        ShellVM shellVM;

        #endregion

        public DailyVM()
        {
            Task.Run(async () => { await LoadCommitments(); }).GetAwaiter().GetResult();
        }

        #region Commands

        async Task LoadCommitments()
        {
            Commitments = await _model.GroupCommitmentsbyDateTime();
        }

        [RelayCommand]
        async Task Check(CommitmentsModel commit)
        {
            commit.Status = !commit.Status;

            await _model.Edit(commit);

            Commitments = await _model.GroupCommitmentsbyDateTime();
        }

        [RelayCommand]
        async Task NewDaily()
        {
            await Shell.Current.Navigation.PushAsync(ShellVM.NewDailyView);
        }

        [RelayCommand]
        async Task CommitDataPopup(int id)
        {
            CommitmentsModel Commit = await _model.GetCommitment(id);

            Popup popup = new CommitmentPopup(new() { Commitment = Commit, ShellVM = ShellVM });

            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        #endregion
    }
}