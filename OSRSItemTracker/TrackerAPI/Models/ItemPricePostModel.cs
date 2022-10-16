namespace TrackerAPI.Models
{
    public class ItemPricePostModel
    {

        public long ItemId { get; set; }
        public long BuyPriceValue { get; set; }
        public long BuyPriceTime { get; set; }
        public long SellPriceValue { get; set; }
        public long SellPriceTime { get; set; }

        public ItemPricePostModel()
        {
        }
    }
}
