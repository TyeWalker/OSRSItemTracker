namespace TrackerAPI.Entities
{
    public class ItemEntity
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ExamineText { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string IsMembers { get; set; } = "False";
        public long HighAlchValue { get; set; }
        public long LowAlchValue { get; set; }
        public long Value { get; set; }
        public int BuyLimit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ItemEntity()
        {

        }
    }
}
