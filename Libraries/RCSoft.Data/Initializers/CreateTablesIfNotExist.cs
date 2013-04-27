using System.Data.Entity;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.Infrastructure;

namespace RCSoft.Data.Initializers
{
    public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        private readonly string[] _tablesToValidate;
        private readonly string[] _customCommands;

        public CreateTablesIfNotExist(string[] tablesTovalidate, string[] customCommands)
            : base()
        {
            this._tablesToValidate = tablesTovalidate;
            this._customCommands = customCommands;
        }

        public void InitializeDatabase(TContext context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }
            if (dbExists)
            {
                bool createTables = false;
                if (_tablesToValidate != null && _tablesToValidate.Length > 0)
                {
                    //一些表验证
                    var existingTableNames = new List<string>(context.Database.SqlQuery<string>("SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'"));
                    createTables = existingTableNames.Intersect(_tablesToValidate, StringComparer.InvariantCultureIgnoreCase).Count() == 0;

                }
                else
                {
                    //检查是否已经创建表
                    int numberOfTables = 0;
                    foreach (var t1 in context.Database.SqlQuery<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE' "))
                        numberOfTables = t1;
                    createTables = numberOfTables == 0;
                }

                if (createTables)
                {
                    //创建所有的表
                    var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                    context.Database.ExecuteSqlCommand(dbCreationScript);

                    context.SaveChanges();

                    if (_customCommands != null && _customCommands.Length > 0)
                    {
                        foreach (var command in _customCommands)
                        {
                            context.Database.ExecuteSqlCommand(command);
                        }
                    }
                }
            }
            else
            {
                throw new ApplicationException("没有数据库实例");
            }
        }
    
    }
}
