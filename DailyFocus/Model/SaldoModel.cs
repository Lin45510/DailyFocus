using DailyFocus.DataBase.DAO;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.Model
{
    [Table("saldo")]

    public class SaldoModel
    {
        #region DataBase Property

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Saldo { get; set; }

        #endregion

        #region Properties

        private readonly SaldoDAO _saldoDAO = new();

        #endregion

        public async Task<SaldoModel> GetSaldo()
        {
            List<SaldoModel> saldoList = await _saldoDAO.GetItemsAsync();

            return saldoList.Count > 0 ? saldoList.Last() : new() { Date = DateTime.Now, Saldo = 0.0 };
        }

        public async Task<double> SaldoValue()
        {
            List<SaldoModel> saldoList = await _saldoDAO.GetItemsAsync();

            return saldoList.Count > 0 ? saldoList[0].Saldo : 0.0;
        }

        public async Task Save(SaldoModel item)
        {
            List<SaldoModel> saldoList = await _saldoDAO.GetItemsAsync();
            item.Id = saldoList.Count > 0 ? 1 : 0;
            await _saldoDAO.SaveItemAsync(item);
        }
    }
}
