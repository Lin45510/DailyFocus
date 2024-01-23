using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyFocus.Model;
using DailyFocus.Resources.DataTemplateSelectors;
using DailyFocus.View;
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


        #region Observable Properties

        [ObservableProperty]
        List<DataSelectorProperties> carrouselTemplates = new()
        {
            new DataSelectorProperties(){Type = "BillsToPay"},
            new DataSelectorProperties(){Type = "BillsToReceive"},
        };

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstopay = new();

        [ObservableProperty]
        ObservableCollection<FinanceModel> billstoreceive = new();

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
            Billstopay = await _model.GetFinancesbyType(0);
            Billstoreceive = await _model.GetFinancesbyType(1);
        }

        [RelayCommand]
        public async Task NewFinance()
        {
            await Shell.Current.Navigation.PushAsync(new NewFinance(new() { finnanceVM = this }));
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

        #endregion
    }
}
