namespace RCSoft.Services.Customers
{
    public partial interface ICustomerAuthenticationService
    {
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="usernameOrEmail">用户名或Email</param>
        /// <param name="password">密码</param>
        /// <returns>结果</returns>
        bool ValidateCustomer(string usernameOrEmail, string password);
    }
}
