namespace Mango.MessageBus
{
    public interface IMessageBus
    {

        Task PublishMessage(BaseMessages message, string topicName);

    }
}