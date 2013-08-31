using System.Collections.Generic;

namespace RCSoft.Services.Authentication.External
{
    public partial class AuthorizationResult
    {
        public AuthorizationResult(OpenAuthenticationStatus status)
        {
            this.Errors = new List<string>();
            Status = status;
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public bool Success
        {
            get { return this.Errors.Count == 0; }
        }

        public OpenAuthenticationStatus Status { get; private set; }

        public List<string> Errors { get; set; }
    }
}
