namespace TrackerAPI.Entities
{
    public class PriceEntity
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long HighPrice { get; set; }
        public DateTime HighTime { get; set; }
        public long LowPrice { get; set; }
        public DateTime LowTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public PriceEntity()
        {
        }
    }
}
