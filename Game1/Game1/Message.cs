using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Game1
{
    public enum MessageType
    {
        Type_A,
        Type_B,
        Type_C,
        Type_D
    }
    public struct Message
    {
        public MessageType Type;
        public int IntValue;
        public float FloatValue;
        public Vector3 V3Value;
        public Quaternion QValue;
        //public GameObject GameObjectValue;
    }
}
