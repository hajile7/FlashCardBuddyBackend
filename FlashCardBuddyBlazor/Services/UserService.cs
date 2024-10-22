using System.Net.Http.Json;
using FlashCardBuddyBlazor.Models;

namespace FlashCardBuddyBlazor.Services 
{
    public class UserService
    {
        private readonly HttpClient _httpclient;
        
        private readonly string url = "https://localhost:7244";

        public UserService(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var result = await _httpclient.GetFromJsonAsync<List<UserDTO>>($"{url}/api/User");
            return result ?? new List<UserDTO>();
        }

    }
}