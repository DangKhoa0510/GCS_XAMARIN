using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GCS_CPC_EMEC.Models;
using Microsoft.EntityFrameworkCore;

namespace GCS_CPC_EMEC.Services
{
   public class DVI_QUANLYReposity : IDataStore<DVI_QUANLY>
    {
        public GCS_Dbcontext _Dbcontext;
        public DVI_QUANLYReposity(GCS_Dbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public async Task<bool> AddItemAsync(DVI_QUANLY item)
        {
            var tracking = await _Dbcontext.Dvi_QUANLYS.AddAsync(item);
            await _Dbcontext.SaveChangesAsync();

            var isAdded = tracking.State == EntityState.Added;

            return isAdded;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                var product = await _Dbcontext.Dvi_QUANLYS.FindAsync(id);

                var tracking = _Dbcontext.Remove(product);

                await _Dbcontext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<DVI_QUANLY> GetItemAsync(string id)
        {
            try
            {
                var product = await _Dbcontext.Dvi_QUANLYS.FindAsync(id);

                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<DVI_QUANLY>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var products = await _Dbcontext.Dvi_QUANLYS.ToListAsync().ConfigureAwait(false);

                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(DVI_QUANLY item)
        {
            try
            {
                var tracking = _Dbcontext.Update(item);

                await _Dbcontext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;

                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public async Task<bool> TruncateTableAsync()
        {
            _Dbcontext.Dvi_QUANLYS.RemoveRange(_Dbcontext.Dvi_QUANLYS);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

       
    }
}

