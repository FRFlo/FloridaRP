using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public class Character
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("owner")]
        public User Owner { get; private set; }
        [Description("firstname")]
        public string FirstName { get; private set; }
        [Description("lastname")]
        public string LastName { get; private set; }
        [Description("birthdate")]
        public DateTime Birthdate { get; private set; }
        [Description("job")]
        public string Job { get; private set; }
        [Description("job_grade")]
        public int JobGrade { get; private set; }
        [Description("accounts")]
        public Account[] Accounts { get; private set; }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
