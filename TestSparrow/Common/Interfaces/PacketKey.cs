using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    class PacketKey
    {
        private int __Id;

        public PacketKey(int id)
        {
            __Id = id;
        }

        public int Id { get { return __Id; } }

        public static PacketKey GetKey(byte[] packetData)
        {
            var id = BitConverter.ToInt32(packetData, 0);
            return new PacketKey(id);
        }
        
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            PacketKey key = obj as PacketKey;
            return __Id == key.__Id;
        }

        public static implicit operator int(PacketKey x)
        {
            return x.__Id;
        }

        public static explicit operator int(PacketKey x)
        {
            return x.__Id;
        }

        public static bool operator ==(PacketKey x, PacketKey y) 
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return __Id.GetHashCode();
        }

        public override string ToString()
        {
            return "ID: " + __Id.ToString();
        }
    }
}
