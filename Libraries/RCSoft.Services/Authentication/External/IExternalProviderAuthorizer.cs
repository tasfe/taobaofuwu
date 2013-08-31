
namespace RCSoft.Services.Authentication.External
{
    public partial interface IExternalProviderAuthorizer
    {
        AuthorizeState Authorize(string returnUrl, string code);
    }
}
