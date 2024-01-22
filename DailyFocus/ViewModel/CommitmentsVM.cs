using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.DataBase.DAO;
using DailyFocus.Model;
using DailyFocus.View;
using DailyFocus.View.PopUp;
using DailyFocus.ViewModel.PopUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.ViewModel
{
    public partial class CommitmentsVM : ObservableObject
    {
        private readonly CommitmentsModel _model = new();
        public IPopupService popupservice;
        public CultureInfo Culture => new CultureInfo("pt-BR");

        #region Obervable Properties

        [ObservableProperty]
        DateTime selectedDate = DateTime.Now.Date;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        ObservableCollection<CommitmentsModel> commitments;

        [ObservableProperty]
        ShellVM shellVM;

        #endregion

        public CommitmentsVM()
        {
            Task.Run(async () => { await LoadCommitments(); }).GetAwaiter().GetResult();
        }

        #region Commands

        [RelayCommand]
        async Task NewCommit()
        {
            await Shell.Current.Navigation.PushAsync(new NewCommitment(new() { date = SelectedDate.ToString("dd/MM/yyyy"), shellVM = ShellVM }));
        }

        [RelayCommand]
        async Task LoadCommitments()
        {
            Commitments = await _model.GroupCommitmentsbyDate();
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
