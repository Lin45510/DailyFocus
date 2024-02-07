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
using System.Globalization;
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
        List<DataSelectorProperties> carrouselTemplates = new();

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstopay = [];

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstoreceive = [];

        [ObservableProperty]
        ObservableCollection<FinanceModel> statement = [];

        [ObservableProperty]
        SaldoModel saldo;

        [ObservableProperty]
        ShellVM shellVM;

        #endregion

        public FinnanceVM()
        {
            Task.Run(async () => { await LoadFinances(); }).GetAwaiter().GetResult();

            carrouselTemplates.Add(new DataSelectorProperties() { Type = "BillsToPay" });
            carrouselTemplates.Add(new DataSelectorProperties() { Type = "BillsToReceive" });
            carrouselTemplates.Add(new DataSelectorProperties() { Type = "Statement" });
        }

        #region Functions

        public async Task LoadFinances()
        {
            Billstopay = await _model.GroupFinancesbyMonth(0);
            Billstoreceive = await _model.GroupFinancesbyMonth(1);
            Statement = await _model.GroupFinancesbyMonth(4);

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

            SaldoModel FinanceSaldo = new()
            {
                Date = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")),
                Saldo = finance.Type == 0 && finance.Status ? Saldo.Saldo - finance.Value :
                        finance.Type == 0 && !finance.Status ? Saldo.Saldo + finance.Value :
                        finance.Type == 1 && finance.Status ? Saldo.Saldo + finance.Value :
                        Saldo.Saldo - finance.Value
            };

            await _saldoModel.Save(FinanceSaldo);

            if (finance.Status)
            {
                FinanceModel newFinance = new()
                {
                    Type = finance.Type == 0 ? 3 : 2,
                    Name = finance.Type == 0 ? string.Concat("Pagamento: ", finance.Name) : string.Concat("Recebimento: ", finance.Name),
                    Value = finance.Value,
                    Date = finance.Date,
                    Month = finance.Month,
                    PaidDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    FinanceID = finance.Id,
                };

                Console.WriteLine(newFinance.PaidDate);

                await _model.Save(newFinance);
            }
            else
            {
                ObservableCollection<FinanceModel> financeModels = await _model.GetFinancesbyType();

                FinanceModel newFinance = financeModels.Where(x => x.FinanceID == finance.Id).First();

                await _model.Delete(newFinance);
            }

            Billstopay = await _model.GroupFinancesbyMonth(0);
            Billstoreceive = await _model.GroupFinancesbyMonth(1);
            Statement = await _model.GroupFinancesbyMonth(4);

            Saldo = await _saldoModel.GetSaldo();
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
            Popup popup = new FinancePopup(new() { ShellVM = ShellVM, Finance = fin });

            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        #endregion
    }
}
