using RCSoft.Core.Domain.Customers;

namespace RCSoft.Services.Authentication
{
    /// <summary>
    /// 身份认证接口
    /// </summary>
    public partial  interface IAuthenticationService
    {
        void SignIn(Customer customer, bool createPersistentCookie);
        void SignOut();
        Customer GetAuthenticateCustomer();
    }
}
