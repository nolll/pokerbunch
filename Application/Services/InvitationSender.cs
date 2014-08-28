using Core.Entities;

namespace Application.Services
{
    public class InvitationSender : IInvitationSender
    {
	    private readonly IMessageSender _messageSender;

        public InvitationSender(
            IMessageSender messageSender)
	    {
	        _messageSender = messageSender;
	    }

	    public void Send(Bunch bunch, Player player, string email)
        {
			var subject = InvitationMessageBuilder.GetSubject(bunch);
			var body = InvitationMessageBuilder.GetBody(bunch, player);
			_messageSender.Send(email, subject, body);
		}

	}
}