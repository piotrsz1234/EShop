
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EShop.Implementations.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EShop.Implementations.EF.Contexts.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }
    } 
}