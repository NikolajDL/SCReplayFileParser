using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ReplayParser.Loader
{

    public class Common
    {
        private Common()
        {

        }

        public static short ReverseBytes(short value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }
        public static int ReverseBytes(int value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }
        public static long ReverseBytes(long value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }

        public static int ToInteger(byte[] bytes)
        {
            int value = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                value <<= 8;
                value = value | (bytes[i] & 0xFF);
            }

            return ReverseBytes(value);
        }

        public static short ToUnsignedByte(byte value)
        {
            return (short)(value & 0xff);
        }

        public static short ToUnsignedByte(int value)
        {
            return (short)(((byte)value) & 0xff);
        }
    }
}
