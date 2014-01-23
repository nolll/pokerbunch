using Core.Classes;

namespace Core.Services{

	public class RegistrationConfirmationSender : IRegistrationConfirmationSender
    {
	    private readonly IMessageSender _messageSender;
	    private readonly IRegistrationConfirmationMessageBuilder _registrationConfirmationMessageBuilder;

	    public RegistrationConfirmationSender(
            IMessageSender messageSender,
            IRegistrationConfirmationMessageBuilder registrationConfirmationMessageBuilder)
	    {
	        _messageSender = messageSender;
	        _registrationConfirmationMessageBuilder = registrationConfirmationMessageBuilder;
	    }

	    public void Send(User user, string password){
            var subject = _registrationConfirmationMessageBuilder.GetSubject();
            var body = _registrationConfirmationMessageBuilder.GetBody(password);
			_messageSender.Send(user.Email, subject, body);
		}

	}
}