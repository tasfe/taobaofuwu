using Rhino.Mocks;
using NUnit.Framework;
using System.Security.Principal;

namespace RCSOFT.Tests
{
    public abstract class TestBase
    {
        protected MockRepository mocks;

        [SetUp]
        public virtual void Setup()
        {
            mocks = new MockRepository();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (mocks != null)
            {
                mocks.ReplayAll();
                mocks.VerifyAll();
            }
        }

        protected static IPrincipal CreatePrincipal(string name, params string[] roles)
        {
            return new GenericPrincipal(new GenericIdentity(name, "TestIdentity"), roles);
        }
    }
}
