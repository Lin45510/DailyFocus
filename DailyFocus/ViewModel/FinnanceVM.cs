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
        ObservableCollection<FinanceModel> finances = new();

        #endregion

        public FinnanceVM()
        {
            Task.Run(async () => { await LoadFinances(); }).GetAwaiter().GetResult();
        }

        #region Functions

        private async Task LoadFinances()
        {
            Finances = await _model.GroupFinancesByDateTime();
        }

        [RelayCommand]
        public async Task NewFinance()
        {
            await Shell.Current.Navigation.PushAsync(new NewFinance());
        }

        #endregion
    }
}
