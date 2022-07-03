using TrackerAPI.Entities;

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

        public PriceEntity Create(PricePostModel model)
        {
            if (model.ItemId == null || model.ItemId == 0)
                return null;

            var priceEntity = new PriceEntity()
            {
                ItemId = model.ItemId,
                HighPrice = model.HighPrice,
                HighTime = DateTime.Parse(model.HighTime),
                LowPrice = model.LowPrice,
                LowTime = DateTime.Parse(model.LowTime),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            return priceEntity;
        }

        public PriceEntity Patch(PriceEntity priceEntity, PricePostModel model)
        {
            priceEntity.ItemId = model.ItemId == 0 ? priceEntity.ItemId : model.ItemId;
            priceEntity.HighPrice = model.HighPrice;
            priceEntity.LowPrice = model.LowPrice;
            priceEntity.HighTime = DateTime.Parse(model.HighTime);
            priceEntity.LowTime = DateTime.Parse(model.LowTime);
            priceEntity.ModifiedDate = DateTime.Now;

            return priceEntity;
        }

        public PriceEntity Put(PriceEntity priceEntity, PricePostModel model)
        {
            if (model.ItemId == 0)
                return null;

            priceEntity.ItemId = model.ItemId == 0 ? priceEntity.ItemId : model.ItemId;
            priceEntity.HighPrice = model.HighPrice;
            priceEntity.LowPrice = model.LowPrice;
            priceEntity.HighTime = DateTime.Parse(model.HighTime);
            priceEntity.LowTime = DateTime.Parse(model.LowTime);
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
    }
}
