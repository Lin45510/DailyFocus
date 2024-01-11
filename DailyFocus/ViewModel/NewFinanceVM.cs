using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.ViewModel
{
    public partial class NewFinanceVM : ObservableObject
    {
        private readonly FinanceModel _model = new();

        private string Month = DateTime.Now.ToString("MMMM", new CultureInfo("pt-BR"));

        #region ObservableProperties

        [ObservableProperty]
        string financeName = "";

        [ObservableProperty]
        DateTime financeDate = new(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        [ObservableProperty]
        double financeValue = 0.0;

        [ObservableProperty]
        List<string> typeOptions;

        [ObservableProperty]
        int financeType = 0;


        #endregion

        public NewFinanceVM()
        {
            typeOptions = _model.TypeOption;
        }

        #region Commands

        [RelayCommand]
        async Task SaveFinance()
        {
            Console.WriteLine(FinanceDate.ToString("dd/MM/yyyy hh:mm"));
            FinanceModel finance = new()
            {
                Id = 0,
                Status = false,
                Name = FinanceName,
                Date = FinanceDate.ToString("dd/MM/yyyy"),
                Month = FinanceDate.ToString("MMMM", new CultureInfo("pt-BR")),
                Type = FinanceType,
                Value = FinanceValue
            };
            await _model.Save(finance);
            FinanceName = "";
            FinanceDate = new(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            FinanceType = 0;
            FinanceValue = 0.0;
        }

        #endregion
    }
}
