namespace Core.Services.Interfaces
{
    public interface IMessageSender
    {
        void Send(string to, IMessage message);
    }
}