using DailyFocus.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.DataBase.DAO
{
    public class CommitmentsDAO
    {
        SQLiteAsyncConnection _connection;

        public CommitmentsDAO()
        {

        }

        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _connection.CreateTableAsync<CommitmentsModel>();
        }
        public async Task<List<CommitmentsModel>> GetItemsAsync()
        {
            await Init();
            return await _connection.Table<CommitmentsModel>().ToListAsync();
        }

        public async Task<CommitmentsModel> GetItemAsync(int id)
        {
            await Init();
            return await _connection.Table<CommitmentsModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(CommitmentsModel item)
        {
            await Init();
            if (item.Id != 0)
                return await _connection.UpdateAsync(item);
            else
                return await _connection.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(CommitmentsModel item)
        {
            await Init();
            return await _connection.DeleteAsync(item);
        }

    }
}
