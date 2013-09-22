namespace Infrastructure.Services{

	public interface IMessageSenderFactory{

		IMessageSender GetMessageSender();

	}

}