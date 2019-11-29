using App10.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App10.Data
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public LocalDatabase(string filepath)
        {
            database = new SQLiteAsyncConnection(filepath);
            database.CreateTableAsync<BestTimes>().Wait();
        
        }

      

        public async Task<List<BestTimes>> GetTime()
        {
            return await database.Table<BestTimes>().ToListAsync();
        }

        public async Task<List<T>> GetTimesAsync<T>() where T : class, new()
        {
            return await database.Table<T>().ToListAsync();
        }

       

        public async Task<T> GetTimeByIdAsync<T>(int id) where T : class, ISqliteModel, new()
        {
            return await database.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveTimeAsync<T>(T item) where T: class, ISqliteModel, new()
        {
            var result = await database.UpdateAsync(item);

            if (result == 0)
            {
                result = await database.InsertAsync(item);
            }

            return result;
        }

        public async Task<int> DeleteItemsAsync<T>()
        {


            return await database.DeleteAllAsync<T>();
        }

    }
}
