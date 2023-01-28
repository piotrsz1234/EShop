using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace EShop.Implementations.EF
{
    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetupColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetupColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetupColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetupColumn(columnModel);
            }
        }

        private static void SetupColumn(ColumnModel column)
        {
            AnnotationValues values;
            if (column.Annotations.TryGetValue("SqlDefaultValue", out values))
            {
                column.DefaultValueSql = (string)values.NewValue;
            }   
        }
    }
}