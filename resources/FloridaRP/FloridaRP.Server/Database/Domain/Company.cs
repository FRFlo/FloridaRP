using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public class Company
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("name")]
        public string Name { get; private set; }
        [Description("owner")]
        public int Owner { get; private set; }
        [Description("created")]
        public DateTime Created { get; private set; }
        [Description("last_seen")]
        public DateTime LastSeen { get; private set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
