using System.Net.Mail;
using Application.Services.Interfaces;

namespace Infrastructure.Services{

	public class MessageSender : IMessageSender{

		public void Send(string to, string subject, string body)
        {
            var message = new MailMessage
                {
                    From = new MailAddress("PokerBunch.com <noreply@pokerbunch.com>"),
                    Subject = subject,
                    Body = body
                };
		    message.To.Add(new MailAddress(to));
 
            var client = new SmtpClient();
            client.Send(message);
		}

	}

}