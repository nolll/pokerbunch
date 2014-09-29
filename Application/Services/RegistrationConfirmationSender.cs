using Core.Entities;

namespace Application.Services{

	public class RegistrationConfirmationSender : IRegistrationConfirmationSender
    {
	    private readonly IMessageSender _messageSender;

	    public RegistrationConfirmationSender(IMessageSender messageSender)
	    {
	        _messageSender = messageSender;
	    }

	    public void Send(User user, string password){
            var message = new RegistrationMessage(password);
			_messageSender.Send(user.Email, message);
		}
	}
}