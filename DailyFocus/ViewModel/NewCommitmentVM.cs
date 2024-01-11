﻿using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.DataBase.DAO;
using DailyFocus.Model;
using DailyFocus.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyFocus.ViewModel
{
    public partial class NewCommitmentVM : ObservableObject
    {
        public string date;
        public bool status;
        public CommitmentsVM commitmentsVM;
        private readonly CommitmentsDAO commitmentsDAO = new();

        #region Observable Properties

        [ObservableProperty]
        int id;

        [ObservableProperty]
        string commitment;

        [ObservableProperty]
        string local;

        [ObservableProperty]
        bool time;

        [ObservableProperty]
        TimeSpan starttime;

        [ObservableProperty]
        TimeSpan endtime;

        #endregion

        public NewCommitmentVM()
        {
            time = false;

            starttime = new TimeSpan(12, 00, 00);
            endtime = new TimeSpan(12, 00, 00);
        }

        #region Commands

        [RelayCommand]
        public async Task Commit()
        {
            if (Commitment == null || Commitment == "")
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alerta", "Insira um nome para o compromisso", "OK");
            }
            else
            {
                CommitmentsModel newcommitment = new()
                {
                    Id = Id,
                    Status = Id != 0 && status,
                    Date = date,
                    Name = Commitment,
                    StartTime = Time ? Starttime.ToString() : "",
                    EndTime = Time ? Endtime.ToString() : "",
                    Local = Local,
                    DayofWeek = DateOnly.ParseExact(date, "dd/MM/yyyy").ToString("dddd", new CultureInfo("pt-BR"))
                };

                await commitmentsDAO.SaveItemAsync(newcommitment);
                Commitment = "";
                Starttime = new(12, 00, 00);
                Endtime = new(12, 00, 00);
                Local = "";
            }
        }

        [RelayCommand]
        public async Task Cancel()
        {
            CommitmentsModel commitmentsModel = new();
            commitmentsVM.Commitments = await commitmentsModel.GroupCommitmentsbyDate();
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion
    }
}