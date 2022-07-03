using Microsoft.EntityFrameworkCore;
using TrackerAPI.Data;
using TrackerAPI.Entities;

namespace TrackerAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context;

        public ItemService(DataContext dataContext)
        {
            _context = dataContext;
        }

        #region Get
        public async Task<List<ItemEntity>> GetAll()
        {
            var items = await _context.Items.ToListAsync();

            if (!items.Any())
                return null;

            return items;
        }

        public async Task<ItemEntity> GetByItemId(long itemId)
        {
            var item = _context.Items.Where(x => x.ItemId == itemId).FirstOrDefault();

            return item;
        }

        public async Task<ItemEntity> GetById(long Id)
        {
            var item = _context.Items.Where(x => x.Id == Id).FirstOrDefault();

            return item;
        }



        #endregion

        #region Create
        public async Task<ItemEntity> Create(ItemEntity model)
        {
            var item = _context.Items.Add(model);
            if (item == null)
                return null;
            await _context.SaveChangesAsync();

            return item.Entity;
        }

        public async Task<List<ItemEntity>> CreateBatch(List<ItemEntity> items)
        {
            _context.Items.AddRange(items);
            await _context.SaveChangesAsync();
            return items;
        }

        #endregion

        #region Update

        public async Task<ItemEntity> Update(ItemEntity model)
        {
            var item = _context.Items.Update(model);
            if (item == null)
                return null;

            await _context.SaveChangesAsync();

            return item.Entity;
        }

        #endregion

        #region Delete

        public async Task<ItemEntity> DeleteById(long id)
        {
            var item = GetById(id);
            if (item == null)
                return null;

            var result = _context.Items.Remove(item.Result);
            if (result == null)
                return null;

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        #endregion
    }
}
