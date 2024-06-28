using Microsoft.AspNetCore.Mvc;
using MovieAPI.Helpers;
using MovieAPI.Models;


namespace MovieAPI.Controllers
{
    public class EmailContoller : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailContoller(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            try
            {
                // E-posta gönderme işlemi
                await _emailService.SendEmailAsync(model.Email, model.Subject, model.Message);

                return Ok("E-posta başarıyla gönderildi.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "E-posta gönderme sırasında bir hata oluştu.");
            }
        }
    }
}

