using Microsoft.EntityFrameworkCore;
using TrackerAPI.Data;
using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public class PriceService : IPriceService
    {
        private readonly DataContext _context;

        public PriceService(DataContext dataContext)
        {
            _context = dataContext;
        }

        #region Get

        public async Task<List<PriceEntity>> GetAll()
        {
            var latestPrices = await _context.LatestPrices.OrderBy(x => x.ItemId).ToListAsync();

            if (!latestPrices.Any())
                return null;

            return latestPrices;
        }

        public async Task<PriceEntity> GetByItemId(long itemId)
        {
            var latestPrice = _context.LatestPrices.Where(x => x.ItemId == itemId).FirstOrDefault();

            return latestPrice;
        }

        public async Task<PriceEntity> GetById(long Id)
        {
            var latestPrice = _context.LatestPrices.Where(x => x.Id == Id).FirstOrDefault();

            return latestPrice;
        }

        public async Task<PriceEntity> VerifyPriceDoesNotExist(PriceEntity newPrice)
        {
            var duplicatePrice = _context.LatestPrices.Where(x => x.PriceValue == newPrice.PriceValue && 
                                                            x.PriceTime == newPrice.PriceTime &&
                                                            x.BuyOrSell == newPrice.BuyOrSell &&
                                                            x.ItemId == x.ItemId).FirstOrDefault();
            return duplicatePrice;
        }

        #endregion

        #region Create
        public async Task<PriceEntity> Create(PriceEntity latestPrice)
        {
            var latestPriceResult = _context.LatestPrices.Add(latestPrice);
            if (latestPriceResult == null)
                return null;

            await _context.SaveChangesAsync();

            return latestPriceResult.Entity;
        }

        #endregion

        #region Update

        public async Task<PriceEntity> Update(PriceEntity model)
        {
            var latestPriceResult = _context.LatestPrices.Update(model);
            if (latestPriceResult == null)
                return null;

            await _context.SaveChangesAsync();

            return latestPriceResult.Entity;
        }

        #endregion

        #region Delete
        public async Task<PriceEntity> DeleteById(long id)
        {
            var item = GetById(id);
            if (item == null)
                return null;

            var latestPriceResult = _context.LatestPrices.Remove(item.Result);
            if (latestPriceResult == null)
                return null;

            await _context.SaveChangesAsync();

            return latestPriceResult.Entity;
        }

        #endregion
    }
}
