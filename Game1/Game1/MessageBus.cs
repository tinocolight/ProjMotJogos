using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class MessageBus
    {
        Dictionary<MessageType, List<MessageSubscriber>> subscriberLists =
        new Dictionary<MessageType, List<MessageSubscriber>>();

        public void AddSubscriber(MessageSubscriber subscriber)
        {
            MessageType[] messageTypes = subscriber.MessageTypes;
            for (int i = 0; i < messageTypes.Length; i++)
                AddSubscriberToMessage(messageTypes[i], subscriber);
        }

        public void AddSubscriberToMessage(MessageType messageType,
                                     MessageSubscriber subscriber)
        {
            if (!subscriberLists.ContainsKey(messageType))
                subscriberLists[messageType] =
                    new List<MessageSubscriber>();

            subscriberLists[messageType].Add(subscriber);
        }

        public void SendMessage(Message message)
        {
            if (!subscriberLists.ContainsKey(message.Type))
                return;

            List<MessageSubscriber> subscriberList =
                subscriberLists[message.Type];

            for (int i = 0; i < subscriberList.Count; i++)
                SendMessageToSubscriber(message, subscriberList[i]);
        }

         public void SendMessageToSubscriber(Message message,
                                     MessageSubscriber subscriber)
        {
            subscriber.Handler.HandleMessage(message);
        }

        
       public static MessageBus instance;

        public static MessageBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessageBus();

                return instance;
            }
        }

        public MessageBus() { }
    }
   
}
