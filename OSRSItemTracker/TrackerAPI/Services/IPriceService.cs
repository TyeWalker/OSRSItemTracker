using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public interface IPriceService
    {
        Task<List<PriceEntity>> GetAll();
        Task<PriceEntity> GetByItemId(long itemId);
        Task<PriceEntity> GetById(long Id);
        Task<PriceEntity> Create(PriceEntity latestPrice);
        Task<PriceEntity> Update(PriceEntity latestPrice);
        Task<PriceEntity> DeleteById(long id);
        Task<PriceEntity> VerifyPriceDoesNotExist(PriceEntity newPrice);
    }
}
