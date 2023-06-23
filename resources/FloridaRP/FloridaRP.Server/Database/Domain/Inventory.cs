using Dapper;
using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public class Inventory
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("type")]
        public string Type { get; private set; }
        [Description("owner")]
        public Character Owner { get; private set; }
        [Description("source")]
        public int Source { get; private set; }
        [Description("slots")]
        public int Slots { get; private set; }
        [Description("maxweight")]
        public int MaxWeight { get; private set; }
        // [Description("items")]
        // public Dictionary<int, Item> Items { get; private set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
