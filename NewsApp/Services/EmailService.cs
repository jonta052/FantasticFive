using NewsApp.Models.Email;
using System.Drawing;

namespace NewsApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConfirmHttp");
        }

        public async Task<string> SendEmail(SubscriptionEmail newEmail)
        {
            var response = await _httpClient.PostAsJsonAsync("ConfirmHttp", newEmail);
            if (response.IsSuccessStatusCode)
            {
                return "Message sent";
            }
            return "Something went wrong";
        }


    }
}
