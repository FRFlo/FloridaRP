using MySqlConnector;

namespace FloridaRP.Server.Database
{
    internal class DatabaseConfiguration
    {
        private static string _connectionString;

        public static string ConnectionString()
        {
            if (!string.IsNullOrEmpty(_connectionString))
                return _connectionString;

            DatabaseConfig databaseConfig = ServerConfiguration.GetDatabaseConfig;

            MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new()
            {
                // Commented out because of some bullcrap with MySQL.Data getting involved due to FluentMigrator.Runner
                // pulling all the bull crap, since 2018 they've talked about fixing it, fixed it May 5th 2022, then not released it
                // https://github.com/fluentmigrator/fluentmigrator/issues/903

                // mySqlConnectionStringBuilder.ApplicationName = databaseConfig.ApplicationName;

                Database = databaseConfig.Database,
                Server = databaseConfig.Server,
                Port = databaseConfig.Port,
                UserID = databaseConfig.Username,
                Password = databaseConfig.Password,

                MaximumPoolSize = databaseConfig.MaximumPoolSize,
                MinimumPoolSize = databaseConfig.MinimumPoolSize,
                ConnectionTimeout = databaseConfig.ConnectionTimeout
            };

            return _connectionString = mySqlConnectionStringBuilder.ToString();
        }
    }
}
