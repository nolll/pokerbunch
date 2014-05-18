using Core.Entities;

namespace Application.Services{

	public class PasswordSender : IPasswordSender
    {
	    private readonly IMessageSender _messageSender;
	    private readonly IPasswordMessageBuilder _passwordMessageBuilder;

	    public PasswordSender(
            IMessageSender messageSender,
            IPasswordMessageBuilder passwordMessageBuilder)
	    {
	        _messageSender = messageSender;
	        _passwordMessageBuilder = passwordMessageBuilder;
	    }

	    public void Send(User user, string password)
	    {
	        var subject = _passwordMessageBuilder.GetSubject();
            var body = _passwordMessageBuilder.GetBody(password);
			_messageSender.Send(user.Email, subject, body);
		}

	}
}