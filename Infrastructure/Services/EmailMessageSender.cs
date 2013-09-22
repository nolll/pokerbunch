using System.Net.Mail;

namespace Infrastructure.Services{

	public class EmailMessageSender : IMessageSender{

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