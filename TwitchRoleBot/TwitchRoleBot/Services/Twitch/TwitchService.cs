using Microsoft.Net.Http.Headers;

namespace TwitchRolesBot.Services.Twitch
{
    public class TwitchSettings
    {
        public string ClientId { get; set; } = "n6ehjc3rmj4s93o0xk72vqgljwnioo";
        public string ClientSecret { get; set; } = "6jbtbvos3odclk1qgwdbxg1jh3a16q";
    }


    public class TwitchService
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly TwitchSettings twitchSettings;
        private string code = string.Empty;

        public TwitchService(ILogger<TwitchService> logger,
            IHttpClientFactory httpClientFactory) 
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            twitchSettings = new TwitchSettings();
        }

        public void SetUserAuthorizationCode(string? code)
        {
            this.code = code ?? string.Empty;
            AuthorizeApplication();
        }

        private void AuthorizeApplication()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://id.twitch.tv/oauth2/token");
            var collection = new List<KeyValuePair<string, string>>
            {
                new("client_id", twitchSettings.ClientId),
                new("client_secret", twitchSettings.ClientSecret),
                new("code", code),
                new("grant_type", "authorization_code"),
                new("redirect_uri", "http://localhost:5034/Login")
            };
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            var client = httpClientFactory.CreateClient();
            var response = client.Send(request);

            var str = response.Content.ReadAsStringAsync().Result;
            logger.LogInformation(str);
        }
    }
}
