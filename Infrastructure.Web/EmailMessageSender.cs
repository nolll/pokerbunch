using System.Net.Mail;
using Application;
using Application.Services;

namespace Infrastructure.Web
{
	public class MessageSender : IMessageSender
    {
        public void Send(string to, IMessage message)
        {
            const string from = "PokerBunch.com <noreply@pokerbunch.com>";

            var email = new MailMessage(from, to, message.Subject, message.Body);

            var client = new SmtpClient();
            client.Send(email);
        }
	}
}