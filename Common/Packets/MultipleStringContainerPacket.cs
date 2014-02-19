﻿using Common.Parsers;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    class MultipleStringContainerPacket: PacketBase
    {
        public static const PacketKey KEY = 0x03;

        private readonly ushort __Length;
        private readonly String[] __MultStrings;

        public MultipleStringContainerPacket(String[] multStrings)
        {
            __MultStrings = multStrings.ToArray();
            __Length = CalculateLength();

        }

        public String[] Strings
        {
            get
            {
                return __MultStrings;
            }
        }

        private ushort CalculateLength()
        {
            if (__MultStrings.Length == 0)
                return 0;

            ushort len = 0;
            foreach (var item in __MultStrings)
            {
                len += (ushort)item.Count((char c) => c == MultipleStringContainerPacketParser.DELIMITER);
            }

            len += (ushort)(__MultStrings.Length - 1);

            return len;
        }


        public override PacketKey Key
        {
            get
            {
                return KEY;
            }
        }
    }
}
