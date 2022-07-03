namespace TrackerAPI.Models
{
    public class PricePostModel
    {
        public long ItemId { get; set; }
        public long HighPrice { get; set; }
        public string HighTime { get; set; }
        public long LowPrice { get; set; }
        public string LowTime { get; set; }
    }
}
