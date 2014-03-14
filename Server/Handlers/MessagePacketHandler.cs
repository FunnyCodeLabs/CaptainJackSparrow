using Common.Packets;
using Communication;
using Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handlers
{
    internal class MessagePacketHandler : ServerPacketHandler
    {
        public MessagePacketHandler(PacketKey key, ServerContext context)
            : base(key, context)
        { }

        public override void Handle(IConnection sender, IPacket packet)
        {
            MessagePacket messagePacket = packet as MessagePacket;
            if (messagePacket == null)
                throw new ServerException("Wrong packet type!");

            UserClient senderClient  = __Context.ClientToConnection.GetByValue(sender);
            if (senderClient.Nickname != messagePacket.From)
                throw new ServerException("Sender nickname does not match to existing UserClient!");

            if (messagePacket.To != null)
            {
                //Private message
                UserClient recipientClient = __Context.ClientToConnection.Keys.FirstOrDefault(c => c.Nickname == messagePacket.To);
                if (recipientClient == null)
                    throw new ServerException("Recipient does not exist!");

                __Context.ClientToConnection.GetByKey(recipientClient).Send(packet);
            }
            else
            {
                NotifyClients(sender, packet);
            }

            __Context.InfoManager.ShowInfo(messagePacket);
        }


    }
}
