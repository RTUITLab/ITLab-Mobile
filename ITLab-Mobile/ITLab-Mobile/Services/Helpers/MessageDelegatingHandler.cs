﻿using IdentityModel.OidcClient;
using System.Net.Http;

namespace ITLab_Mobile.Services.Helpers
{
    public class MessageDelegatingHandler : RefreshTokenDelegatingHandler
    {
        public MessageDelegatingHandler(OidcClient oidcClient, string accessToken, string refreshToken, HttpMessageHandler innerHandler = null)
            : base(oidcClient, accessToken, refreshToken, innerHandler)
        {
            base.TokenRefreshed += MessageDelegatingHandler_TokenRefreshed;
        }

        private void MessageDelegatingHandler_TokenRefreshed(object sender, TokenRefreshedEventArgs e)
        {
            Settings.AccessToken = e.AccessToken;
            Settings.RefreshToken = e.RefreshToken;
        }
    }
}
