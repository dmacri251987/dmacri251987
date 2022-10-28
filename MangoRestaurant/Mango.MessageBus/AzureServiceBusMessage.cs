using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessage : IMessageBus
    {
        //can be improved
        private string connectionString = "Endpoint=sb://mangorestaraunt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kx2rpEIYNGZH6hOqz5FF4L7Y6yiGZ0ww+YXDGEHFe5A=";

        public async Task PublishMessage(BaseMessages message, string topicName)
        {
            ISenderClient senderClient = new TopicClient(connectionString, topicName);

            var JsonMessage = JsonConvert.SerializeObject(message);
            var finalMessage = new Message(Encoding.UTF8.GetBytes(JsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()

            };

            await senderClient.SendAsync(finalMessage);

            await senderClient.CloseAsync();
        }
    }
}
