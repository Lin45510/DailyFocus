using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyFocus.Model;
using SQLite;

namespace DailyFocus.DataBase.DAO
{
    public class FinanceDAO
    {
        SQLiteAsyncConnection _connection;

        public FinanceDAO()
        {

        }

        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _connection.CreateTableAsync<FinanceModel>();
        }
        public async Task<List<FinanceModel>> GetItemsAsync()
        {
            await Init();
            return await _connection.Table<FinanceModel>().ToListAsync();
        }

        public async Task<FinanceModel> GetItemAsync(int id)
        {
            await Init();
            return await _connection.Table<FinanceModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(FinanceModel item)
        {
            await Init();
            if (item.Id != 0)
                return await _connection.UpdateAsync(item);
            else
                return await _connection.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(FinanceModel item)
        {
            await Init();
            return await _connection.DeleteAsync(item);
        }
    }
}
