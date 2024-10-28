using FlashCardBuddy_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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


        [HttpGet]

        public async Task<IActionResult> GetFlashCardId(int flashcardid)
        {
            Flashcard result = await dbContext.Flashcards.FirstOrDefaultAsync(f => f.Flashcardid == flashcardid)
                ?? throw new InvalidOperationException("The response from the database was null.");

            return Ok(result);
        }
        [HttpGet("All")]

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

        [HttpPut]

        public async Task<IActionResult> UpdateFlashCard(Flashcard flashcard)
        {
            Flashcard result = await dbContext.Flashcards.FirstOrDefaultAsync(f => f.Flashcardid == flashcard.Flashcardid)
            ?? throw new InvalidOperationException("The response from the database was null");

            if (flashcard.Question != null)
            {
                result.Question = flashcard.Question;
            }
            if (flashcard.Answer != null)
            {
                result.Answer = flashcard.Answer;
            }
            if (flashcard.Stack != null)
            {
                result.Stack = flashcard.Stack;
            }

            dbContext.Flashcards.Update(result);
            await dbContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpDelete("{flashcardID}")]

        public async Task<IActionResult> DeleteFlashCard(int flashcardID)
        {
            Flashcard result = await dbContext.Flashcards.FirstOrDefaultAsync(f => f.Flashcardid == flashcardID) 
            ?? throw new InvalidOperationException("The response from the database was null.");

            dbContext.Flashcards.Remove(result);
            await dbContext.SaveChangesAsync(); 
            return NoContent();
        }
    }
}
