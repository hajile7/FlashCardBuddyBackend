using FlashCardBuddy_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashCardBuddy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashCardController : ControllerBase
    {
        private FlashCardBuddyDbContext dbContext = new FlashCardBuddyDbContext();

        static FlashCardDTO FlashCardDTOConversion(Flashcard f)
        {
            return new FlashCardDTO

            {
                Question = f.Question,
                Answer = f.Answer,
                Stack = f.Stack,
                Userid = f.Userid,

            };
        }

        [HttpGet("allFlashCards")]

        public async Task<IActionResult> GetAll(int userId)
        {
            List<Flashcard> result = await dbContext.Flashcards.Where(i => i.Userid == userId).ToListAsync();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{stack}")]

        public async Task<IActionResult> GetByStack(string stack, int userId)
        {
            List<Flashcard> result = await dbContext.Flashcards.Where(f => f.Stack == stack && f.Userid == userId).ToListAsync();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Add(FlashCardDTO flashcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Flashcard newFlashCard = new Flashcard();

            newFlashCard.Question = flashcard.Question;
            newFlashCard.Answer = flashcard.Answer;
            newFlashCard.Stack = flashcard.Stack;
            newFlashCard.Userid = flashcard.Userid;

            dbContext.Flashcards.Add(newFlashCard);
            await dbContext.SaveChangesAsync();

            return Ok(newFlashCard);
        }


    }
}
