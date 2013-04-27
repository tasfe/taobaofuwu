using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Web.Hosting;
using System.IO;
using System;
using System.Text;
using System.Data.Entity;
using RCSoft.Data.Initializers;
using System.Data.SqlClient;
using System.Data.Common;

namespace RCSoft.Data
{
    public class SqlServerDataProvider:BaseEfDataProvider
    {
        /// <summary>
        /// 获取连接工厂
        /// </summary>
        /// <returns>连接工厂类</returns>
        public override IDbConnectionFactory GetConnectionFactory()
        {
            return new SqlConnectionFactory();
        }

        /// <summary>
        /// 设置数据库初始化容器
        /// </summary>
        public override void SetDatabaseInitializer()
        {
            //跳过一些验证，以免覆盖旧版本数据
            var tablesToVSalidate = new[] { "Customer", "Discount", "Order", "Product" };

            //

            var customCommands = new List<string>();

            customCommands.AddRange(ParseCommands(HostingEnvironment.MapPath("~/App_Data/SqlServer.Indexes.sql"), false));
            customCommands.AddRange(ParseCommands(HostingEnvironment.MapPath("~/App_Data/SqlServer.StoredProcedures.sql"), false));

            var initializer = new CreateTablesIfNotExist<RCSoftObjectContext>(tablesToVSalidate, customCommands.ToArray());
            Database.SetInitializer(initializer);
        }

        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                {
                    throw new ArgumentException(string.Format("文件:'{0}'不存在！", filePath));
                }
                else
                {
                    return new string[0];
                }
            }

            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                var statement = "";
                while ((statement = readNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }

        protected virtual string readNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();
            string lineOfText;
            while (true)
            {
                lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();
                    else
                        return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool StoredPeoceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
