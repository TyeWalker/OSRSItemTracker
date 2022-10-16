using System.ComponentModel.DataAnnotations;

namespace TrackerAPI.Models
{
    public class PricePostModel
    {
        public long ItemId { get; set; }
        public long PriceValue { get; set; }
        public long PriceTime { get; set; }
        public string BuyOrSell { get; set; } = string.Empty;

        public PricePostModel()
        {
        }
    }
}
