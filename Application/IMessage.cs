namespace Application
{
    public interface IMessage
    {
        string Subject { get; }
        string Body { get; }
    }
}