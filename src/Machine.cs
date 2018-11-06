using DbUp;

namespace karl_oskar 
{
    public class Machine 
    {
        private string _connectionString;
        private string _scriptDirectory;

        public Machine(string cs, string dir) 
        {
            _connectionString = cs;
            _scriptDirectory = dir;
        }

        public void Run() 
        {
            EnsureDatabase.For.PostgresqlDatabase(_connectionString);

            var upgrader =
                DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsFromFileSystem(_scriptDirectory)
                .LogToConsole()
                .LogScriptOutput()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful) {
                throw result.Error;
            }
        }
    }
}