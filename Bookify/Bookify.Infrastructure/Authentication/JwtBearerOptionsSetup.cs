﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly AuthenticationOptions _authenticationOptions;

        public JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions)
        {
            _authenticationOptions = authenticationOptions.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.Audience = _authenticationOptions.Audience;
            options.MetadataAddress = _authenticationOptions.MetadataUrl;
            options.RequireHttpsMetadata = _authenticationOptions.RequireHttosMetadata;
            options.TokenValidationParameters.ValidIssuer = _authenticationOptions.Issuer;
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}
