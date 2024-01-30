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

        private readonly SaldoModel _saldomodel = new();

        public ShellVM shellVM;

        #region ObservableProperties

        [ObservableProperty]
        int id;

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
            if (FinanceName == "" || FinanceName == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alerta", "Insira um nome para o compromisso", "OK");
            }
            else
            {
                FinanceModel finance = new()
                {
                    Id = Id,
                    Status = false,
                    Name = FinanceName,
                    Date = FinanceDate.ToString("dd/MM/yyyy"),
                    Month = FinanceDate.ToString("MMMM", new CultureInfo("pt-BR")),
                    Type = FinanceType,
                    Value = FinanceValue,
                };
                await _model.Save(finance);


                if (FinanceType == 2 || FinanceType == 3)
                {
                    double currentsaldo = await _saldomodel.SaldoValue();
                    SaldoModel saldo = new()
                    {
                        Date = DateTime.Now,
                        Saldo = FinanceType == 2 ? currentsaldo + FinanceValue : currentsaldo - FinanceValue
                    };
                    await _saldomodel.Save(saldo);

                }

                FinanceName = "";
                FinanceDate = new(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                FinanceType = 0;
                FinanceValue = 0.0;

                shellVM.FinanceView.FinnanceVM.Billstopay = await _model.GroupFinancesbyMonth(0);
                shellVM.FinanceView.FinnanceVM.Billstoreceive = await _model.GroupFinancesbyMonth(1);
                shellVM.FinanceView.FinnanceVM.Financeentry = await _model.GroupFinancesbyMonth(2);
                shellVM.FinanceView.FinnanceVM.Financeloss = await _model.GroupFinancesbyMonth(3);

                shellVM.FinanceView.FinnanceVM.Saldo = await _saldomodel.GetSaldo();
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion
    }
}
