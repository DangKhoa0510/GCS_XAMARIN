using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GCS_CPC_EMEC.Models;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace GCS_CPC_EMEC.Services
{
  public  class TRAMReposity : IDataStore<TRAM>
    {
        public GCS_Dbcontext _Dbcontext;
        public TRAMReposity(GCS_Dbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public async Task<bool> AddItemAsync(TRAM item)
        {
            var tracking = await _Dbcontext.TRAMS.AddAsync(item);
            await _Dbcontext.SaveChangesAsync();

            var isAdded = tracking.State == EntityState.Added;

            return isAdded;
        }

        public async Task<bool> DeleteItemAsync(string ma_tram)
        {
            try
            {
                var product = await _Dbcontext.TRAMS.FindAsync(ma_tram);

                var tracking = _Dbcontext.TRAMS.Remove(product);

                await _Dbcontext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task< bool> DeleteTram_DonVi(string donvi)
        {
            List<TRAM> trams = await _Dbcontext.TRAMS.Where(p => p.MA_DVIQLY == donvi).ToListAsync().ConfigureAwait(false);
            foreach (TRAM _tram in trams)
            {
                _Dbcontext.TRAMS.Remove(_tram);
            }
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<TRAM> GetItemAsync(string id)
        {
            try
            {
                var product =  _Dbcontext.TRAMS.Where(p=>p.MA_TRAM == id).FirstOrDefault();

                return   product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public  List<TRAM> GetTramTheoDVQLAsync(string ma_donvi) 
        {
            try
            {
                var product = _Dbcontext.TRAMS.Where(p => p.MA_DVIQLY == ma_donvi).ToList();                
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<TRAM>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var products = await _Dbcontext.TRAMS.ToListAsync().ConfigureAwait(false);

                return products; 
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(TRAM item)
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
            _Dbcontext.TRAMS.RemoveRange(_Dbcontext.TRAMS);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
