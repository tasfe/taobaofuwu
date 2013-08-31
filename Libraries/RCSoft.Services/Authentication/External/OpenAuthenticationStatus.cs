
namespace RCSoft.Services.Authentication.External
{
    public enum OpenAuthenticationStatus
    {
        Unknown,
        Error,
        Authenticated,
        RequiresRedirect,
        AssociateOnLogon,
        AutoRegisteredEmailValidation,
        AutoRegistereAdminApproval,
        AutoRegisteredStandard,
    }
}
