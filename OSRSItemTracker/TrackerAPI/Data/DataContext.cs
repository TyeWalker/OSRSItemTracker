using Microsoft.EntityFrameworkCore;
using TrackerAPI.Entities;

namespace TrackerAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<PriceEntity> LatestPrices { get; set; }
        public DbSet<VolumeEntity> Volume { get; set; }
    }
}
