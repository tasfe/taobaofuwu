﻿using System;
using System.Collections.Generic;

namespace RCSoft.Services.Authentication.External
{
    [Serializable]
    public abstract partial class OpenAuthenticationParameters
    {
        public abstract string ProviderSystemName { get; }
        public string ExternalIdentifier { get; set; }
        public string ExternalDisplayIdentifier { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthAccessToken { get; set; }

        public virtual IList<UserClaims> UserClaims
        {
            get { return new List<UserClaims>(0); }
        }
    }
}
