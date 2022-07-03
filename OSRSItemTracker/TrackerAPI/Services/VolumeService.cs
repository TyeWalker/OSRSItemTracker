using Microsoft.EntityFrameworkCore;
using TrackerAPI.Data;
using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly DataContext _context;

        public VolumeService(DataContext dataContext)
        {
            _context = dataContext;
        }

        #region Get
        public async Task<VolumeEntity> GetVolumeEntity(long id)
        {
            var volumeEntity = await _context.Volume.FindAsync(id);
            if (volumeEntity == null)
                return null;
            return volumeEntity;
        }

        public async Task<VolumeEntity> GetLastHourVolumeForItem(long itemId)
        {
            var volumeEntity = _context.Volume.Where(x => x.ItemId == itemId && x.TimeDuration == 60).FirstOrDefault();
            if (volumeEntity == null)
                return null;

            return volumeEntity;
        }

        public async Task<List<VolumeEntity>> GetLastHourForAllItems()
        {
            var dateToSearch = DateTime.Now.AddHours(-1);
            var volumes = _context.Volume.Where(x => x.TimeOfEntry > dateToSearch && x.TimeDuration == 60).ToList();
            if (volumes == null)
                return null;
            return volumes;
        }


        public async Task<VolumeEntity> GetLastDayVolumeForItem(long itemId)
        {
            var dateToSearch = DateTime.Now.AddHours(-24);
            var volumeEntity = _context.Volume.Where(x => x.ItemId == itemId && x.TimeDuration == 1440 && x.TimeOfEntry > dateToSearch).FirstOrDefault();
            if (volumeEntity == null)
                return null;

            return volumeEntity;
        }

        public async Task<VolumeEntity> VerifyVolumeDoesNotExist(VolumeEntity volumeEntity)
        {
            var result = _context.Volume.Where(x => x.ItemId == volumeEntity.ItemId && x.TimeOfEntry == volumeEntity.TimeOfEntry).FirstOrDefault();
            return result;
        }

        #endregion

        #region Create

        public async Task<VolumeEntity> Create(VolumeEntity model)
        {
            var volume = _context.Volume.Add(model);
            if (volume == null)
                return null;
            await _context.SaveChangesAsync();

            return volume.Entity;
        }

        public async Task<List<VolumeEntity>> CreateBatch(List<VolumeEntity> models)
        {
            foreach (var model in models)
            {
                var volume = _context.Volume.Add(model);
                if (volume == null)
                    return null;
            }
            await _context.SaveChangesAsync();
            return models;
        }

        #endregion

        #region Update

        public async Task<VolumeEntity> Update(VolumeEntity model)
        {
            var volume = _context.Volume.Update(model);
            if (volume == null)
                return null;

            await _context.SaveChangesAsync();

            return volume.Entity;
        }

        #endregion

        #region Delete
        public async Task<VolumeEntity> DeleteById(long id)
        {
            var volumeEntity = GetVolumeEntity(id);

            if (volumeEntity == null)
                return null;

            var result = _context.Volume.Remove(volumeEntity.Result);
            if (result == null)
                return null;

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAllEntriesForItem(long itemId)
        {
            if (itemId == 0)
                return;

            var sql = "DELETE FROM Volume WHERE itemId = " + itemId;

            _context.Volume.FromSqlRaw(sql);

            await _context.SaveChangesAsync();

            return;
        }

        #endregion
    }
}
