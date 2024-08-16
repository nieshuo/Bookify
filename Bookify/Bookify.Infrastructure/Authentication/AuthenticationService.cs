using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private const string PasswordCredentialType = "password";
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials = new CredentialRepresentationModel[]
                {
                    new CredentialRepresentationModel
                    {
                        Value = password,
                        Temporary = false,
                        Type = PasswordCredentialType
                    }
                };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "users", 
                userRepresentationModel, 
                cancellationToken
            );

            return ExtractIdentityIdFromLocationHeader(response);
        }

        private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage response)
        {
            const string usersSegmentName = "users/";

            string? locationHander = response.Headers.Location?.PathAndQuery;
            if (locationHander is null)
            {
                throw new InvalidOperationException("Location header can't be null");
            }

            int userSegmentValueIndex = locationHander.IndexOf(
                usersSegmentName,
                StringComparison.InvariantCultureIgnoreCase);

            string userIdentityId = locationHander.Substring(userSegmentValueIndex + usersSegmentName.Length);

            return userIdentityId;
        }
    }
}
