using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public interface IVolumeService
    {
        Task<VolumeEntity> GetVolumeEntity(long id);
        Task<VolumeEntity> GetLastHourVolumeForItem(long itemId);
        Task<VolumeEntity> GetLastDayVolumeForItem(long itemId);
        Task<VolumeEntity> Create(VolumeEntity model);
        Task<VolumeEntity> Update(VolumeEntity model);
        Task<VolumeEntity> DeleteById(long id);
        Task<List<VolumeEntity>> GetLastHourForAllItems();
        Task<List<VolumeEntity>> CreateBatch(List<VolumeEntity> models);
        Task<VolumeEntity> VerifyVolumeDoesNotExist(VolumeEntity volumeEntity);
    }
}
