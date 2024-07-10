using GCS_CPC_EMEC.Models;
using GCS_CPC_EMEC.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCS_CPC_EMEC.Database
{   

    public class InformationsRepository : IDataStore<Information>
    {

        private readonly DatabaseContext _databaseContext;
       public InformationsRepository (DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async  Task<bool> AddItemAsync(Information item)
        {
            var tracking = await _databaseContext.Informations .AddAsync(item);

            await _databaseContext.SaveChangesAsync();

            var isAdded = tracking.State == EntityState.Added;

            return isAdded;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                var product = await _databaseContext.Informations.FindAsync(id);

                var tracking = _databaseContext.Remove(product);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Information> GetItemAsync(string id)
        {
            try
            {
                var product = await _databaseContext.Informations.FindAsync(id);

                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async  Task<IEnumerable<Information>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var products =  _databaseContext.Informations;

                return products.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(Information item)
        {
            try
            {
                var tracking = _databaseContext.Update(item);

                await _databaseContext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;

                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
