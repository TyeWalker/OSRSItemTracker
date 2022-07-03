namespace TrackerAPI.Models
{
    public class PricePostModel
    {
        public long ItemId { get; set; }
        public long HighPrice { get; set; }
        public DateTime HighTime { get; set; }
        public long LowPrice { get; set; }
        public DateTime LowTime { get; set; }
    }
}
