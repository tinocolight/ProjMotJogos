using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class MessageSubscriberController
    {
        public MessageType[] MessageTypes;
        public MessageHandler Handler;

        void Start()
        {
            MessageSubscriber subscriber = new MessageSubscriber();
            subscriber.MessageTypes = MessageTypes;
            subscriber.Handler = Handler;

            MessageBus.Instance.AddSubscriber(subscriber);
        }
    }
}
