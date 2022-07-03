namespace TrackerAPI.Models
{
    public class VolumePostModel
    {
        public long ItemId { get; set; }
        public long AverageHighPrice { get; set; }
        public long AverageHighVolume { get; set; }
        public long AverageLowPrice { get; set; }
        public long AverageLowVolume { get; set; }
        public int TimeDuration { get; set; } // minutes
        public string TimeOfEntry { get; set; }
        public VolumePostModel()
        {
        }
    }
}
