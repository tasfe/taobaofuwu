using System.Collections.Generic;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Services.Authentication.External
{
    public partial interface IOpenAuthenticationService
    {
        IList<IExternalAuthenticationMethod> LoadActiveExternalAuthenticationMethods();

        IExternalAuthenticationMethod LoadExternalAuthenticationMethodBySystemName(string systemName);

        IList<IExternalAuthenticationMethod> LoadAllExternalAuthenticationMethods();


        bool AccountExists(OpenAuthenticationParameters parameters);

        void AssociateExternalAccountWithUser(Customer customer, OpenAuthenticationParameters parameters);

        Customer GetUser(OpenAuthenticationParameters parameters);

        IList<ExternalAuthenticationRecord> GetExternalIdentifiersFor(Customer customer);

        void RemoveAssociation(OpenAuthenticationParameters parameters);
    }
}
