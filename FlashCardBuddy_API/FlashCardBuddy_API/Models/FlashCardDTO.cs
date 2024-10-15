namespace FlashCardBuddy_API.Models
{
    public class FlashCardDTO
    {
        public string Question { get; set; } = null!;

        public string Answer { get; set; } = null!;

        public string? Stack { get; set; }

        public int? Userid { get; set; }
    }
}
