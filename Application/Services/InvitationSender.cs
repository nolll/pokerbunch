using Core.Entities;

namespace Application.Services
{
    public class InvitationSender : IInvitationSender
    {
	    private readonly IMessageSender _messageSender;
        private readonly IInvitationMessageBuilder _invitationMessageBuilder;

        public InvitationSender(
            IMessageSender messageSender,
            IInvitationMessageBuilder invitationMessageBuilder)
	    {
	        _messageSender = messageSender;
	        _invitationMessageBuilder = invitationMessageBuilder;
	    }

	    public void Send(Homegame homegame, Player player, string email)
        {
			var subject = _invitationMessageBuilder.GetSubject(homegame);
			var body = _invitationMessageBuilder.GetBody(homegame, player);
			_messageSender.Send(email, subject, body);
		}

	}
}