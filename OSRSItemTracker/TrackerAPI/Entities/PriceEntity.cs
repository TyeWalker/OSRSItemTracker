using System.ComponentModel.DataAnnotations.Schema;
using TrackerAPI.Enums;

namespace TrackerAPI.Entities
{
    [Table("Price")]
    public class PriceEntity
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long PriceValue { get; set; }
        public DateTime PriceTime { get; set; }
        public PriceType BuyOrSell { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public PriceEntity()
        {
        }
    }
}
