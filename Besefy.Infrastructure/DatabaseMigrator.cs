using System.Reflection;
using DbUp;

namespace Besefy.Infrastructure
{
    public static class DatabaseMigrator
    {
        public static bool Run(string connectionString)
        {
            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Migrations applied successfully.");
            Console.ResetColor();
            return true;
        }
    }
}
