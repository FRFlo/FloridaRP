using FxEvents;
using FloridaRP.Shared;

namespace FloridaRP.Client.Scripts
{
    internal sealed class ClientConnection : ScriptBase
    {
        private static readonly object _padlock = new();
        private static ClientConnection _instance;

        private ClientConnection()
        {
            OnStartupAsync();
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

        internal async void OnStartupAsync()
        {
            try
            {
                bool isConnectionActive = await EventDispatcher.Get<bool>("connection:active");
                if (isConnectionActive)
                    Logger.Info("Connection is active.");
                else
                    Logger.Info("Connection is not active.");
            }
            catch (Exception ex)
            {
                Logger.Error($"---------------------------------------------.");
                Logger.Error($"Client failed to load.");
                Logger.Info($"{ex}");
                Logger.Error($"---------------------------------------------.");
            }
        }
    }
}
