using absolwent.Models;
using System.Net.Mail;

namespace absolwent.Services
{
    public interface IPoolService
    {
        void SendMail(MailMessage Message);
    }
}