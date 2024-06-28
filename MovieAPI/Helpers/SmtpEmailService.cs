using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MovieAPI.Helpers
{
    public class SmtpEmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com") // Gmail SMTP sunucusuna bağlanmak için kullanılır
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@gmail.com", "your-password"),
                    EnableSsl = true, //güvenli bağlantıyı etkinleştirir
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true, //E-posta içeriğinin HTML formatında
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }

            catch (Exception ex)
            {
                // E-posta gönderme hatası
                throw new InvalidOperationException("E-posta gönderme sırasında bir hata oluştu.", ex);
            }
    }   }
}
