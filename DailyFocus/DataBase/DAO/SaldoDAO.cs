using DailyFocus.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.DataBase.DAO
{
    public class SaldoDAO
    {
        SQLiteAsyncConnection _connection;
        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _connection.CreateTableAsync<SaldoModel>();
        }

        public async Task<int> SaveItemAsync(SaldoModel item)
        {
            await Init();
            if (item.Id == 0)
                return await _connection.InsertAsync(item);
            else
                return await _connection.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(SaldoModel item)
        {
            await Init();
            return await _connection.DeleteAsync(item);
        }

        public async Task<List<SaldoModel>> GetItemsAsync()
        {
            await Init();
            return await _connection.Table<SaldoModel>().ToListAsync();
        }
    }
}