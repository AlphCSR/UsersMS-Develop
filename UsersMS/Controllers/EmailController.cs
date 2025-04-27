using Microsoft.AspNetCore.Mvc;
using UsersMS.Core.Service;

namespace UsersMS.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailsController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendEmail(string receptor)
        {
            await emailService.SendEmail(receptor);
            return Ok();
        }

        [HttpPost("send-passsword")]
        public async Task<IActionResult> SendPassword(string receptor, int code)
        {
            await emailService.SendPassword(receptor,code);
            return Ok();
        }
    }
}
