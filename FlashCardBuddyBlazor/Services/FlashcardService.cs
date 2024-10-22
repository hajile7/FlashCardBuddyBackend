using System.Net.Http.Json;
using FlashCardBuddyBlazor.Models;


namespace FlashCardBuddyBlazor.Services
{
    public class FlashcardService
    {
        private readonly HttpClient _httpclient;
        
        private readonly string url = "https://localhost:7244";

        public FlashcardService(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<Flashcard>> GetFlashCardsAsync(int id)
        {
            var result = await _httpclient.GetFromJsonAsync<List<Flashcard>>($"{url}/api/Flashcard/All?userId={id}");
            return result ?? new List<Flashcard>();
        }
    }
}