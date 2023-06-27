using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public enum OwnerType
    {
        User,
        Company,
        None
    }
    public class Account
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("ownertype")]
        public OwnerType OwnerType { get; private set; }
        [Description("ownerid")]
        public int OwnerId { get; private set; }
        [Description("name")]
        public string Name { get; private set; }
        [Description("created")]
        public DateTime Created { get; private set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
    public class AccountTransaction
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("sender")]
        public int Sender { get; private set; }
        [Description("receiver")]
        public int Receiver { get; private set; }
        [Description("amount")]
        public int Amount { get; private set; }
        [Description("date")]
        public DateTime Date { get; private set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
