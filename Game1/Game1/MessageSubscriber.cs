using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public struct MessageSubscriber
    {
        public MessageType[] MessageTypes;
        public MessageHandler Handler;
    }
}
