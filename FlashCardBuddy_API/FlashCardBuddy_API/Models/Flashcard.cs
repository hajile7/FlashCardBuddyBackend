using System;
using System.Collections.Generic;

namespace FlashCardBuddy_API.Models;

public partial class Flashcard
{
    public int Flashcardid { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string? Stack { get; set; }

    public int? Userid { get; set; }

}
