using Microsoft.AspNetCore.Mvc;
using TwitchRolesBot.Services.Twitch;

namespace TwitchRolesBot.ApiControllers
{
    [ApiController]
    [Route("Login")]
    public class AuthorizationController : ControllerBase
    {
        private readonly TwitchService twitchService;
        private readonly ILogger<AuthorizationController> logger;

        public AuthorizationController(TwitchService twitchService,
                                       ILogger<AuthorizationController> logger)
        {
            this.twitchService = twitchService;
            this.logger = logger;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] string? code,
                                 [FromQuery] string? scope,
                                 [FromQuery] string? error,
                                 [FromQuery] string? error_description)
        {
            if (string.IsNullOrEmpty(error))
                twitchService.SetUserAuthorizationCode(code);

            return Ok();
        }
    }
}
