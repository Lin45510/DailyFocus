using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using DailyFocus.Resources.DataTemplateSelectors;
using DailyFocus.View.PopUp;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DailyFocus.ViewModel
{
    public partial class NewDailyVM : ObservableObject
    {

        private readonly CommitmentsModel _model = new();

        #region Observable Properties

        [ObservableProperty]
        ShellVM shellVM;

        [ObservableProperty]
        ObservableCollection<CommitmentsModel> commitments;

        [ObservableProperty]
        ObservableCollection<CommitmentsModel> noTimeCommitments;

        [ObservableProperty]
        List<DataSelectorProperties> carrouselTemplates = new();

        [ObservableProperty]
        DateTime selectedDate = DateTime.Now.Date;

        [ObservableProperty]
        string commitName;

        [ObservableProperty]
        TimeSpan startTime;

        [ObservableProperty]
        TimeSpan endTime;

        [ObservableProperty]
        string local;

        #endregion

        public NewDailyVM()
        {
            carrouselTemplates.Add(new DataSelectorProperties() { Type = "New" });
            carrouselTemplates.Add(new DataSelectorProperties() { Type = "Planning" });
            carrouselTemplates.Add(new DataSelectorProperties() { Type = "NoTime" });

            startTime = new TimeSpan(12, 00, 00);
            endTime = new TimeSpan(12, 00, 00);

            Task.Run(async () => { await LoadCommitments(); }).GetAwaiter().GetResult();

        }

        #region Commands

        async Task LoadCommitments()
        {
            Commitments = await _model.GetCommitmentsOnDate(SelectedDate.ToString("dd/MM/yyyy"));
            NoTimeCommitments = await _model.GetCommitmentsOnDate(SelectedDate.ToString("dd/MM/yyyy"), false);
        }

        [RelayCommand]
        async Task ChangeDay()
        {
            await LoadCommitments();
        }

        [RelayCommand]
        public void CarousellScrollNext(CarouselView carousel)
        {
            int position = carousel.Position + 1;

            carousel.ScrollTo(position, 0);
        }

        [RelayCommand]
        public void CarousellScrollPrevious(CarouselView carousel)
        {
            int position = carousel.Position - 1;

            carousel.ScrollTo(position, 0);
        }

        [RelayCommand]
        async Task NewCommit()
        {
            if (CommitName == null || CommitName == "")
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alerta", "Insira um nome para o compromisso", "OK");
            }
            else
            {
                CommitmentsModel newCommit = new()
                {
                    Id = 0,
                    Status = false,
                    Date = SelectedDate.ToString("dd/MM/yyyy"),
                    Name = CommitName,
                    StartTime = StartTime.ToString(),
                    EndTime = EndTime.ToString(),
                    Local = Local,
                    DayofWeek = DateOnly.ParseExact(SelectedDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy").ToString("dddd", new CultureInfo("pt-BR"))
                };

                await _model.Edit(newCommit);
                CommitName = "";
                StartTime = new TimeSpan(12, 00, 00);
                EndTime = new TimeSpan(12, 00, 00);
                Local = "";

                await LoadCommitments();

                ShellVM.DailyView.DailyVM.Commitments = await _model.GroupCommitmentsbyDateTime();
                ShellVM.CommitmentsView.CommitmentsVM.Commitments = await _model.GroupCommitmentsbyDate();
            }
        }

        [RelayCommand]
        async Task CommitDataPopup(int id)
        {
            CommitmentsModel Commit = await _model.GetCommitment(id);

            Popup popup = new CommitmentPopup(new() { Commitment = Commit, ShellVM = ShellVM, NewDailyVM = this });

            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        #endregion
    }
}
