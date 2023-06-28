using Dapper;
using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public class Character
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("owner")]
        public int OwnerId { get; set; }
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

        public async Task<User> GetOwnerAsync() => await User.GetUser(OwnerId);

        public static Task<Character> GetCharacter(int characterId)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pId", characterId);

            return Dapper<Character>.GetSingleAsync("call selCharacterById(@pId);", dynamicParameters);
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
