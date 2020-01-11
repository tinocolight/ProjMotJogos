using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Game1
{
    public abstract class MessageHandler
    {
         public abstract void HandleMessage(Message message);
    }
}
