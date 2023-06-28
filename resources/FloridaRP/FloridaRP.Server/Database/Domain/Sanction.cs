using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public enum SanctionType
    {
        Warn,
        Ban,
        Kick,
        Mute
    }
    public class Sanction
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("user")]
        public int UserId { get; set; }
        [Description("type")]
        public SanctionType Type { get; private set; }
        [Description("reason")]
        public string Reason { get; private set; }
        [Description("created")]
        public DateTime Created { get; private set; }
        [Description("expires")]
        public DateTime Expires { get; private set; }

        public async Task<User> GetUserAsync() => await User.GetUser(UserId);

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
