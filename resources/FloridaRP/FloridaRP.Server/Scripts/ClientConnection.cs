using FloridaRP.Server.Database.Domain;
using FloridaRP.Server.Models;
using FloridaRP.Shared;
using FxEvents;
using FxEvents.Shared.TypeExtensions;

namespace FloridaRP.Server.Scripts
{
    public class ClientConnection : ScriptBase
    {
        private static readonly object _padlock = new();
        private static ClientConnection _instance;

        private ClientConnection()
        {
            EventDispatcher.Mount("connection:active", new Func<Player, Task<bool>>(OnConnectionActiveAsync));
        }

        internal static ClientConnection Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new ClientConnection();
                }
            }
        }

        private async Task<bool> OnConnectionActiveAsync([FromSource] Player player)
        {
            try
            {
                Logger.Info($"Player {player.Name} is active.");

                User user = await User.GetUser(player);

                Session session = new(player.Handle.ToInt());
                session.SetUser(user);

                Main.ActiveSessions.TryAdd(session.Handle, session);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"OnConnectionActiveAsync() Exception");
                Logger.Info($"{ex}");
                Logger.Error($"OnConnectionActiveAsync() Exception");
                return false;
            }
        }
    }
}
