using absolwent.Models;
using System.Net;
using System.Net.Mail;

namespace absolwent.Services
{
    public class PoolService : IPoolService
    {

        public void StartPool(User user, PoolSettings pool)
        {
            var message = new MailMessage("absolwent.best@gmail.com", user.Email, "Nowa ankieta", $"Witaj, Nowe ankiety będą wysyłane co {pool.Frequency} miesiące.");
            SendMail(message);
        }
        public void StartPool()
        {
            var message = new MailMessage("absolwent.best@gmail.com", "michu.penk@gmail.com", "Test", "Wiadomość testowa");
            SendMail(message);
        }
        public static void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp-relay.sendinblue.com"; // smtp.googlemail.com
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential(
                "absolwent.best@gmail.com",
                "RngFmUGJqbp4jVtk");
            //"Az%jWm6Sjy7f3*A6");
            client.Send(Message);
        }

    }
}
