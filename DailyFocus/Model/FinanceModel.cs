using DailyFocus.DataBase.DAO;
using DailyFocus.View;
using DailyFocus.ViewModel;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DailyFocus.Model
{
    [Table("finance")]
    public class FinanceModel
    {
        #region DataBase Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int FinanceID { get; set; }
        public string PaidDate { get; set; }
        public int Type { get; set; } // 0 - À Pagar || 1 - À Receber || 2 - Entrada || 3 Saida
        public double Value { get; set; }

        #endregion

        #region Properties

        [Ignore]
        public List<FinanceModel> FinancesOnMounth { get; set; }

        public ICommand Command;

        private readonly FinanceDAO financeDAO = new();

        private readonly SaldoDAO saldoDAO;

        [Ignore]
        public List<string> TypeOption
        {
            get { return ["À Pagar", "À Receber", "Entrada", "Saida"]; }
            private set {; }
        }

        #endregion

        #region Functions

        public async Task<ObservableCollection<FinanceModel>> GroupFinancesbyMonth(int type = -1)
        {
            ObservableCollection<FinanceModel> commitmentsByDate = [];

            ObservableCollection<FinanceModel> finList = await GetFinancesbyType(type);

            IEnumerable<IGrouping<string, FinanceModel>> query =
                finList.GroupBy(x => x.Month, x => x);

            foreach (IGrouping<string, FinanceModel> finGroup in query)
            {
                List<FinanceModel> financesonMonth = [];

                foreach (FinanceModel name in finGroup)
                {
                    financesonMonth.Add(new FinanceModel
                    {
                        Id = name.Id,
                        Status = name.Status,
                        Name = name.Name,
                        Date = name.Date,
                        Month = name.Month,
                        Type = name.Type,
                        Year = name.Year,
                        Value = name.Value,
                        PaidDate = name.PaidDate,
                    });
                }

                List<FinanceModel> Finances = [.. financesonMonth.OrderBy(x => DateTime.Parse(string.Join(x.Date, "12:00:00 AM")))];

                commitmentsByDate.Add(
                    new FinanceModel
                    {
                        Month = finGroup.Key,
                        FinancesOnMounth = Finances
                    });
            }
            return commitmentsByDate;
        }

        public async Task<ObservableCollection<FinanceModel>> GetFinancesbyType(int Type = -1)
        {
            ObservableCollection<FinanceModel> financesByType = new();
            List<FinanceModel> finances = new();

            List<FinanceModel> finList = await financeDAO.GetItemsAsync();

            finances = Type switch
            {
                0 => finList.Where(x => x.Type == 0).ToList(),
                1 => finList.Where(x => x.Type == 1).ToList(),
                2 => finList.Where(x => x.Type == 2).ToList(),
                3 => finList.Where(x => x.Type == 3).ToList(),
                4 => finList.Where(x => x.Type == 2 || x.Type == 3).ToList(),
                _ => finList,
            };
            foreach (FinanceModel finance in finances)
            {
                financesByType.Add(finance);
            }

            return financesByType;
        }

        public async Task Save(FinanceModel finance)
        {
            await financeDAO.SaveItemAsync(finance);
        }

        public async Task<FinanceModel> GetFinance(int id)
        {
            FinanceModel finance = await financeDAO.GetItemAsync(id);

            return finance;
        }

        public async Task Delete(FinanceModel finance)
        {
            await financeDAO.DeleteItemAsync(finance);
        }

        public async Task Edit(FinanceModel finance)
        {
            await financeDAO.SaveItemAsync(finance);
        }

        public async Task EditPopUp(FinanceModel finance, ShellVM shellVM)
        {
            DateOnly financedate = DateOnly.Parse(finance.Date, new CultureInfo("pt-BR"));

            await Shell.Current.Navigation.PushAsync(new NewFinance(new()
            {
                Id = finance.Id,
                FinanceName = finance.Name,
                FinanceType = finance.Type,
                FinanceDate = new(
                              financedate.Year,
                              financedate.Month,
                              DateTime.DaysInMonth(financedate.Year, financedate.Month)),
                FinanceValue = finance.Value,
                shellVM = shellVM
            }));
        }

        #endregion
    }
}
