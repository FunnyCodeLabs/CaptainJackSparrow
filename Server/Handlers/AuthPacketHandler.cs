using Common;
using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handlers
{
    internal class AuthPacketHandler : ServerPacketHandler
    {
        public AuthPacketHandler(ServerContext context)
            : base(context)
        { }

        public override void Handle(IConnection sender, IPacket packet)
        {
            AuthPacket authPacket = packet as AuthPacket;
            Contract.Requires<ArgumentException>(authPacket == null);

            switch (authPacket.Status)
            {
                case AuthStatus.Online:
                    ProcessOnline(sender, authPacket);
                    break;
                case AuthStatus.AFK:
                    ProcessAFK(sender, authPacket);
                    break;
                case AuthStatus.Offline:
                    ProcessOffline(sender, authPacket);
                    break;
                default:
                    throw new NotSupportedException();
            }

            __Context.InfoManager.ShowInfo(authPacket);
        }

        private void ProcessOffline(IConnection sender, AuthPacket authPacket)
        {
            UserClient client;
            if (__Context.ClientToConnection.ContainsValue(sender))
            {
                client = ProcessExistingClient(sender, authPacket);

                if (client.Status == AuthStatus.Offline)
                    throw new NotSupportedException("Duplicate of AuthPacket with status Offline!");

                __Context.ClientToConnection.Remove(client);
                __Context.InfoManager.ShowInfo(String.Format("User with nickname {0} offline.", authPacket.Nickname));
            }
            else
            {
                throw new NotSupportedException("Client doest not exist!");
            }

            AuthInformationPacket info = new AuthInformationPacket(authPacket as AuthPacket);
            NotifyClients(sender, info);
        }

        private void ProcessAFK(IConnection sender, AuthPacket authPacket)
        {
            UserClient client;
            if (__Context.ClientToConnection.ContainsValue(sender))
            {
                client = ProcessExistingClient(sender, authPacket);

                __Context.InfoManager.ShowInfo(String.Format("User with nickname {0} afk.", authPacket.Nickname));
            }
            else
            {
                throw new NotSupportedException("Client doest not exist!");
            }

            AuthInformationPacket info = new AuthInformationPacket(authPacket as AuthPacket);
            NotifyClients(sender, info);
        }

        private void ProcessOnline(IConnection sender, AuthPacket authPacket)
        {
            UserClient client;
            if (__Context.ClientToConnection.ContainsValue(sender))
            {
                client = ProcessExistingClient(sender, authPacket);

                if (client.Status == AuthStatus.Online)
                    throw new NotSupportedException("Duplicate of AuthPacket with status Online!");

                __Context.InfoManager.ShowInfo(String.Format("User with nickname {0} online.", authPacket.Nickname));
            }
            else
            {
                client = new UserClient(authPacket.Nickname, authPacket.Status);
                __Context.ClientToConnection.Add(client, sender);

                __Context.InfoManager.ShowInfo(String.Format("User with nickname {0} connected.", authPacket.Nickname));
            }

            AuthInformationPacket info = new AuthInformationPacket(authPacket as AuthPacket);
            NotifyClients(sender, info);
        }

        private UserClient ProcessExistingClient(IConnection sender, AuthPacket authPacket)
        {
            UserClient client;
            client = __Context.ClientToConnection.GetByValue(sender);

            __Context.InfoManager.ShowInfo(String.Format("User with nickname {0} {1}.", authPacket.Nickname, authPacket.Status));
            client.Status = authPacket.Status;
            return client;
        }
    }
}
