using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public interface IItemService
    {
        Task<List<ItemEntity>> GetAll();
        Task<ItemEntity> GetByItemId(long itemId);
        Task<ItemEntity> GetById(long id);
        Task<ItemEntity> Create(ItemEntity model);
        Task<ItemEntity> Update(ItemEntity model);
        Task<ItemEntity> DeleteById(long id);
        Task<List<ItemEntity>> CreateBatch(List<ItemEntity> items);
    }
}
