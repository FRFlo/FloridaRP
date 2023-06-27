using Dapper;
using FxEvents.Shared;

namespace FloridaRP.Server.Database.Domain
{
    public class User
    {
        [Description("id")]
        public int Id { get; private set; }
        [Description("name")]
        public string Name { get; private set; }

        [Description("character")]
        public int CharacterId { get; set; }

        [Description("created")]
        public DateTime Created { get; private set; }

        [Description("last_seen")]
        public DateTime LastSeen { get; private set; }

        public async Task<Sanction> GetActiveBan()
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pUserId", Id);

            return await Dapper<Sanction>.GetSingleAsync("call selUserActiveBan(@pUserId);", dynamicParameters);
        }

        public static async Task<User> GetUser(Player player)
        {
            // Get player tokens
            int numTokens = GetNumPlayerTokens(player.Handle);
            List<string> tokens = new();
            for (int i = 0; i < numTokens; i++)
            {
                tokens.Add(GetPlayerToken(player.Handle, i));
            }

            // Get player identifiers
            int numIdentifiers = GetNumPlayerIdentifiers(player.Handle);
            List<string> identifiers = new();
            for (int i = 0; i < numIdentifiers; i++)
            {
                string identifier = GetPlayerIdentifier(player.Handle, i);

                if (identifier.StartsWith("ip:")) continue;

                identifiers.Add(GetPlayerIdentifier(player.Handle, i));
            }

            string fivem = identifiers.FirstOrDefault(x => x.StartsWith("fivem:"));
            string steam = identifiers.FirstOrDefault(x => x.StartsWith("steam:"));
            string discord = identifiers.FirstOrDefault(x => x.StartsWith("discord:"));
            string license2 = identifiers.FirstOrDefault(x => x.StartsWith("license2:"));
            string license = identifiers.FirstOrDefault(x => x.StartsWith("license:"));
            string live = identifiers.FirstOrDefault(x => x.StartsWith("live:"));
            string xbl = identifiers.FirstOrDefault(x => x.StartsWith("xbl:"));

            // find user using fivem identifier
            User user = await OnGetUserByIdentityAsync(fivem);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by fivem identifier.");
                return user;
            }
            // find user using steam identifier
            user = await OnGetUserByIdentityAsync(steam);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by steam identifier.");
                return user;
            }
            // find user using discord identifier
            user = await OnGetUserByIdentityAsync(discord);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by discord identifier.");
                return user;
            }
            // find user using license2 identifier
            user = await OnGetUserByIdentityAsync(license2);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by license2 identifier.");
                return user;
            }
            // find user using license identifier
            user = await OnGetUserByIdentityAsync(license);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by license identifier.");
                return user;
            }
            // find user using xbl identifier
            user = await OnGetUserByIdentityAsync(xbl);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by xbl identifier.");
                return user;
            }
            // find user using live identifier
            user = await OnGetUserByIdentityAsync(live);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by live identifier.");
                return user;
            }

            // find user using tokens
            user = await OnGetUserByTokensAsync(tokens);
            if (user is not null)
            {
                Main.Logger.Debug($"Found user {user.Name} by token.");
                return user;
            }

            // if no user is returned, then create one.
            if (user is null)
            {
                user = await OnInsertUserAsync(player.Name);

                foreach (string token in tokens)
                    await user.OnInsertTokenAsync(token);

                foreach (string identity in identifiers)
                {
                    string[] ident = identity.Split(':');
                    await user.OnInsertIdentityAsync(ident[0], ident[1]);
                }

                return user;
            }

            return default;
        }

        #region Private methods
        private async Task OnInsertTokenAsync(string token)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pUserId", Id);
            dynamicParameters.Add("pToken", token);

            await Dapper<User>.ExecuteAsync("call insUserToken(@pUserId, @pToken);", dynamicParameters);
        }

        private async Task OnInsertIdentityAsync(string type, string value)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pUserId", Id);
            dynamicParameters.Add("pType", type);
            dynamicParameters.Add("pValue", value);

            await Dapper<User>.ExecuteAsync("call insUserIdentity(@pUserId, @pType, @pValue);", dynamicParameters);
        }

        private static async Task<User> OnGetUserByIdentityAsync(string identity)
        {
            if (string.IsNullOrEmpty(identity)) return null;

            string[] ident = identity.Split(':');
            string type = ident[0];
            string value = ident[1];

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pType", type);
            dynamicParameters.Add("pValue", value);

            return await Dapper<User>.GetSingleAsync("call selUserByIdentity(@pType, @pValue);", dynamicParameters);
        }

        private static async Task<User> OnGetUserByTokensAsync(List<string> tokens)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pTokens", string.Join(",", tokens));

            return await Dapper<User>.GetSingleAsync("call selUserByToken(@pTokens);", dynamicParameters);
        }

        private static async Task<User> OnInsertUserAsync(string Name)
        {
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("pName", Name);

            return await Dapper<User>.GetSingleAsync("call insUser(@pName);", dynamicParameters);
        }
        #endregion

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
