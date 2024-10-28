namespace FlashCardBuddy_API.Models
{
    public class UserDTO
    {
        public int Userid { get; set; }
        public string Username { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
