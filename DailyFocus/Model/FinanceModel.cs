using DailyFocus.DataBase.DAO;
using SQLite;
using System;
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
        public int Type { get; set; } // 0 - À Pagar || 1 - À Receber
        public double Value { get; set; }

        #endregion

        #region Properties

        [Ignore]
        public List<FinanceModel> FinancesOnDate { get; set; }

        public ICommand Command;

        private readonly FinanceDAO financeDAO = new();

        [Ignore]
        public List<string> TypeOption
        {
            get { return ["À Pagar", "À Receber"]; }
            private set {; }
        }

        #endregion

        #region Functions

        public async Task<ObservableCollection<FinanceModel>> GroupFinancesByDateTime()
        {

            ObservableCollection<FinanceModel> financesOnDate = new();

            List<FinanceModel> finList = await financeDAO.GetItemsAsync();

            Console.WriteLine("DOING ;-;");

            List<FinanceModel> financesByDateTime = finList.OrderBy(x => DateTime.Parse(x.Date + "12:00")).ToList();

            Console.WriteLine("DONE!!");

            foreach (FinanceModel finance in financesByDateTime)
            {
                financesOnDate.Add(finance);
            }

            return financesOnDate;
        }

        public async Task Save(FinanceModel finance)
        {
            await financeDAO.SaveItemAsync(finance);
        }

        #endregion
    }
}
