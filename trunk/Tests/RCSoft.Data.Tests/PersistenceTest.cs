using NUnit.Framework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RCSoft.Data.Tests
{
    [TestFixture]
    public abstract class PersistenceTest
    {
        protected RCSoftObjectContext context;

        [SetUp]
        public void SetUp()
        {
            Database.DefaultConnectionFactory = new SqlConnectionFactory();
            context = new RCSoftObjectContext(GetTestDbName());
        }

        protected string GetTestDbName()
        {
            string testDbName = "Data Source=" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)) + @"\\Nop.Data.Tests.Db.sdf;Persist Security Info=False";
            return testDbName;
        }        
        
    }
}
