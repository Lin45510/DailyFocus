using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using DailyFocus.Resources.DataTemplateSelectors;
using DailyFocus.View;
using DailyFocus.View.PopUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.ViewModel
{
    public partial class FinnanceVM : ObservableObject
    {
        private readonly FinanceModel _model = new();
        private readonly SaldoModel _saldoModel = new();


        #region Observable Properties

        [ObservableProperty]
        List<DataSelectorProperties> carrouselTemplates =
        [
            new DataSelectorProperties() { Type = "BillsToPay" },
            new DataSelectorProperties() { Type = "BillsToReceive" },
            new DataSelectorProperties() { Type = "FinanceEntry" },
            new DataSelectorProperties() { Type = "FinanceLoss" },
        ];

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstopay = [];

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstoreceive = [];

        [ObservableProperty]
        ObservableCollection<FinanceModel> financeentry = [];

        [ObservableProperty]
        ObservableCollection<FinanceModel> financeloss = [];

        [ObservableProperty]
        SaldoModel saldo;

        [ObservableProperty]
        ShellVM shellVM;

        #endregion

        public FinnanceVM()
        {
            Task.Run(async () => { await LoadFinances(); }).GetAwaiter().GetResult();
        }

        #region Functions

        public async Task LoadFinances()
        {
            Billstopay = await _model.GroupFinancesbyMonth(0);
            Billstoreceive = await _model.GroupFinancesbyMonth(1);
            Financeentry = await _model.GroupFinancesbyMonth(2);
            Financeloss = await _model.GroupFinancesbyMonth(3);

            Saldo = await _saldoModel.GetSaldo();
        }

        [RelayCommand]
        public async Task NewFinance()
        {
            await Shell.Current.Navigation.PushAsync(new NewFinance(new() { shellVM = ShellVM }));
        }

        [RelayCommand]
        public async Task Check(FinanceModel finance)
        {
            finance.Status = !finance.Status;

            await _model.Edit(finance);

            Billstopay = await _model.GetFinancesbyType(0);
            Billstoreceive = await _model.GetFinancesbyType(1);
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
        async Task FinanceDataPopup(FinanceModel fin)
        {
            //FinanceModel finance = await _model.GetFinance(fin.Id);

            Popup popup = new FinancePopup(new() { ShellVM = ShellVM, Finance = fin });

            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        #endregion
    }
}
