using System.Web;
using RCSoft.Core.Infrastructure;
using System.Collections.Generic;

namespace RCSoft.Services.Authentication.External
{
    public static partial class ExternalAuthorizerHelper
    {
        private static HttpSessionStateBase GetSession()
        {
            var session = EngineContext.Current.Resolve<HttpSessionStateBase>();
            return session;
        }

        public static void StroeParametersForRoundTrip(OpenAuthenticationParameters parameters)
        {
            var session = GetSession();
            session["RCSoft.externalauth.parameters"] = parameters;
        }

        public static OpenAuthenticationParameters RetrieveParameterFromRoundTrip(bool removeOnRetrieval)
        {
            var session = GetSession();
            var parameters = session["RCSoft.externalauth.parameters"];
            if (parameters != null && removeOnRetrieval)
                RemoveParameters();
            return parameters as OpenAuthenticationParameters;
        }

        public static void RemoveParameters()
        {
            var session = GetSession();
            session.Remove("RCSoft.externalauth.parameters");
        }

        public static void RemoveParameters(string error)
        {
            var session = GetSession();
            var errors = session["RCSoft.externalauth.errors"] as IList<string>;
            if (errors == null)
            {
                errors = new List<string>();
                session.Add("RCSoft.externalauth.errors", errors);
            }
            errors.Add(error);
        }

        public static IList<string> RetrieveErrorsToDisplay(bool removeOnRetrieval)
        {
            var session = GetSession();
            var errors = session["RCSoft.externalauth.errors"] as IList<string>;
            if (errors != null && removeOnRetrieval)
                session.Remove("RCSoft.externalauth.errors");
            return errors;
        }
    }
}