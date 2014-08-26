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

	    public void Send(Homegame homegame, Player player, string email)
        {
			var subject = InvitationMessageBuilder.GetSubject(homegame);
			var body = InvitationMessageBuilder.GetBody(homegame, player);
			_messageSender.Send(email, subject, body);
		}

	}
}