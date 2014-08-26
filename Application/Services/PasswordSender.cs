using Core.Entities;

namespace Application.Services{

	public class PasswordSender : IPasswordSender
    {
	    private readonly IMessageSender _messageSender;

	    public PasswordSender(
            IMessageSender messageSender)
	    {
	        _messageSender = messageSender;
	    }

	    public void Send(User user, string password)
	    {
	        var subject = PasswordMessageBuilder.GetSubject();
            var body = PasswordMessageBuilder.GetBody(password);
			_messageSender.Send(user.Email, subject, body);
		}

	}
}