namespace TrackerAPI.Models
{
    public class UserAuthModel
    {
        public string UserName { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
