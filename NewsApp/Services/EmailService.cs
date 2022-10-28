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
            var response = await _httpClient.PostAsJsonAsync("ConfirmHttp?code=fLNpR62m3yIlY_zbr9SN1xSAoU89_tXPgqiWOWWa1IinAzFui-qGcQ==", newEmail);
            //var response = await _httpClient.PostAsJsonAsync("ConfirmHttp?code=qP-IJRepG7C6UaHJS3T4x1yoUnvCAQa2MBHt8-0UT1lQAzFuAgpohw==", newEmail);
            if (response.IsSuccessStatusCode)
            {
                return "Message sent";
            }
            return "Something went wrong";
        }
       

    }
}
