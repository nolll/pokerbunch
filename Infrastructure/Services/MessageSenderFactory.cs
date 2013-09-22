namespace Infrastructure.Services{

	public class MessageSenderFactory : IMessageSenderFactory
    {
	    public IMessageSender GetMessageSender(){
			return new EmailMessageSender();
		}

	}

}