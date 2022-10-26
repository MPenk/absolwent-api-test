using absolwent.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace absolwent.Services
{
    public class PoolService : IPoolService
    {
        private AppSettings _appSettings;

        public PoolService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "mail.mpenk.pl";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(
                "noreplay@absolwent.best",
                _appSettings.MailPassword);
            client.Send(Message);
        }

    }
}
