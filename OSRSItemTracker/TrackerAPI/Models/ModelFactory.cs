using TrackerAPI.Entities;
using TrackerAPI.Enums;

namespace TrackerAPI.Models
{
    public class ModelFactory
    {
        public ModelFactory()
        {

        }
        #region Item
        public ItemEntity Create(ItemPostModel itemModel)
        {
            // validation
            if (itemModel.Name == null || itemModel.ItemId == 0)
                return null;

            var itemEntity = new ItemEntity()
            {
                ItemId = itemModel.ItemId,
                Name = itemModel.Name ?? string.Empty,
                ExamineText = itemModel.ExamineText ?? string.Empty,
                IconName = itemModel.IconName?? string.Empty,
                IsMembers = itemModel.IsMembers,
                HighAlchValue = itemModel.HighAlchValue,
                LowAlchValue = itemModel.LowAlchValue,
                Value = itemModel.Value,
                BuyLimit = itemModel.BuyLimit,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            return itemEntity;
        }

        public ItemEntity Patch(ItemEntity itemEntity, ItemPostModel itemModel)
        {
            if (itemModel.Name.Equals(string.Empty) || itemModel.Name == null)
                return null;

            itemEntity.ItemId = itemModel.ItemId == 0 ? itemEntity.ItemId : itemModel.ItemId;
            itemEntity.Name = itemModel.Name ?? itemEntity.Name;
            itemEntity.ExamineText = itemModel.ExamineText ?? itemEntity.ExamineText;
            itemEntity.IconName = itemModel.IconName ?? itemEntity.IconName;
            itemEntity.IsMembers = itemModel.IsMembers ?? itemEntity.IsMembers;
            itemEntity.HighAlchValue = itemModel.HighAlchValue == -1 ? itemEntity.HighAlchValue : itemModel.HighAlchValue;
            itemEntity.LowAlchValue = itemModel.LowAlchValue == -1 ? itemEntity.LowAlchValue : itemModel.LowAlchValue;
            itemEntity.Value = itemModel.Value == -1 ? itemEntity.Value : itemModel.Value;
            itemEntity.BuyLimit = itemModel.BuyLimit == -1 ? itemEntity.BuyLimit : itemModel.BuyLimit;
            itemEntity.ModifiedDate = DateTime.Now;

            return itemEntity;
        }

        public ItemEntity Put(ItemEntity itemEntity, ItemPostModel itemModel)
        {
            if (itemModel.Name.Equals(string.Empty) || itemModel.Name == null || itemModel.ItemId == 0)
                return null;

            itemEntity.ItemId = itemModel.ItemId;
            itemEntity.Name = itemModel.Name ?? string.Empty;
            itemEntity.ExamineText = itemModel.ExamineText ?? string.Empty;
            itemEntity.IconName = itemModel.IconName ?? string.Empty;
            itemEntity.IsMembers = itemModel.IsMembers;
            itemEntity.HighAlchValue = itemModel.HighAlchValue;
            itemEntity.LowAlchValue = itemModel.LowAlchValue;
            itemEntity.Value = itemModel.Value;
            itemEntity.BuyLimit = itemModel.BuyLimit;
            itemEntity.ModifiedDate = DateTime.Now;

            return itemEntity;
        }


        #endregion

        #region Price

        public PriceEntity[] Create(ItemPricePostModel model)
        {
            if (model.ItemId == null || model.ItemId == 0)
                return null;

            PriceEntity[] priceEntities = new PriceEntity[2];

            var buyPriceEntity = new PriceEntity()
            {
                ItemId = model.ItemId,
                PriceValue = model.BuyPriceValue,
                PriceTime = toDateTime(model.BuyPriceTime),
                BuyOrSell = PriceType.Buy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
            priceEntities[0] = buyPriceEntity;

            var sellPriceEntity = new PriceEntity()
            {
                ItemId = model.ItemId,
                PriceValue = model.SellPriceValue,
                PriceTime = toDateTime(model.SellPriceTime),
                BuyOrSell = PriceType.Sell,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
            priceEntities[1] = sellPriceEntity;

            return priceEntities;
        }

        public PriceEntity Patch(PriceEntity priceEntity, PricePostModel model)
        {
            priceEntity.ItemId = model.ItemId == 0 ? priceEntity.ItemId : model.ItemId;
            priceEntity.PriceValue = model.PriceValue;
            priceEntity.PriceTime = toDateTime(model.PriceTime);
            priceEntity.BuyOrSell = toPriceType(model.BuyOrSell);
            priceEntity.ModifiedDate = DateTime.Now;

            return priceEntity;
        }

        public PriceEntity Put(PriceEntity priceEntity, PricePostModel model)
        {
            if (model.ItemId == 0)
                return null;

            priceEntity.ItemId = model.ItemId == 0 ? priceEntity.ItemId : model.ItemId;
            priceEntity.PriceValue = model.PriceValue;
            priceEntity.PriceTime = toDateTime(model.PriceTime);
            priceEntity.BuyOrSell = toPriceType(model.BuyOrSell);
            priceEntity.ModifiedDate = DateTime.Now;

            return priceEntity;
        }


        #endregion

        #region Volume

        public VolumeEntity Create(VolumePostModel model)
        {
            if (model.ItemId == 0)
                return null;

            VolumeEntity volumeEntity = new VolumeEntity()
            {
                ItemId = model.ItemId,
                AverageHighPrice = model.AverageHighPrice,
                AverageHighVolume = model.AverageHighVolume,
                AverageLowVolume = model.AverageLowVolume,
                AverageLowPrice = model.AverageLowPrice,
                TimeDuration = model.TimeDuration,
                TimeOfEntry = Convert.ToDateTime(model.TimeOfEntry),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            return volumeEntity;
        }

        public VolumeEntity Put(VolumeEntity volumeEntity, VolumePostModel model)
        {
            volumeEntity.ItemId = model.ItemId == 0 ? volumeEntity.ItemId : model.ItemId;
            volumeEntity.AverageHighPrice = model.AverageHighPrice;
            volumeEntity.AverageHighVolume = model.AverageHighVolume;
            volumeEntity.AverageLowPrice = model.AverageLowPrice;
            volumeEntity.AverageLowVolume = model.AverageLowVolume;
            volumeEntity.TimeDuration = model.TimeDuration;
            volumeEntity.TimeOfEntry = Convert.ToDateTime(model.TimeOfEntry);
            volumeEntity.ModifiedDate = DateTime.Now;

            return volumeEntity;
        }

        #endregion

        #region utils

        public DateTime toDateTime(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public PriceType toPriceType(string input)
        {
            PriceType priceType;
            Enum.TryParse(input, out priceType);
            return priceType;
        }

        #endregion
    }    
}
