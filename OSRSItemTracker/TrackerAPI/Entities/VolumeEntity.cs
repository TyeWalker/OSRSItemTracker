namespace TrackerAPI.Entities
{
    public class VolumeEntity
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long AverageHighPrice { get; set; }
        public long AverageHighVolume { get; set; }
        public long AverageLowPrice { get; set; }
        public long AverageLowVolume { get; set; }
        public int TimeDuration { get; set; }
        public DateTime TimeOfEntry { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public VolumeEntity()
        {
        }
    }
}
