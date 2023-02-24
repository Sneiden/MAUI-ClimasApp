using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climas.Context
{
    public class DataContext
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db is not null)
                return;

            db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "Climas.db3"),
                SQLiteOpenFlags.ReadWrite
                | SQLiteOpenFlags.Create
                | SQLiteOpenFlags.SharedCache
              );

            _ = await db.CreateTableAsync<Models.City>();
        }

        public async Task<List<Models.City>> GetCities()
        {
            await Init();
            return await db.Table<Models.City>().ToListAsync();
        }

        public async Task<Models.City> GetCityById(int id)
        {
            await Init();
            return await db.Table<Models.City>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SetCity(Models.City city)
        {
            await Init();
            var _car = await GetCityById(city.Id);

            if (_car is not null)
                return false;

            return await db.InsertAsync(city) == 1;
        }

        public async Task<bool> RemoveFromCities(int id)
        {
            await Init();
            var _city = await GetCityById(id);

            if (_city is null)
                return false;

            return await db.DeleteAsync(_city) == 1;
        }

    }
}
