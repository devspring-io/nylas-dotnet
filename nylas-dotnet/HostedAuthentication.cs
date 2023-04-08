
namespace Nylas
{
    /// <summary>
    /// Handles the interaction with the Nylas API for the hosted authentication flow.
    /// </summary>
    public class HostedAuthentication
    {
        public NylasApplication Application { get; private set; }

        public HostedAuthentication(NylasApplication nylasApplication)
        {
            Application = nylasApplication;
        }

        /// <summary>
        /// Wraps the Nylas "Send Authentication Code" API call for the hosted authentication flow.
        /// See https://developer.nylas.com/docs/api/#post/oauth/token
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns>Returns the access token from Nylas on success. On failure it returns null.</returns>
        public async Task<AccessToken?> FetchToken(string? authorizationCode)
        {
            if (authorizationCode == null)
            {
                throw new ArgumentNullException("authorizationCode cannot be null");
            }

            Uri tokenUrl = Application.NylasClient.CreateUrl("oauth/token");
            AuthorizationCodeRequest authCodeRequest = new AuthorizationCodeRequest
            {
                client_id = Application.ClientId,
		        client_secret = Application.ClientSecret,
		        grant_type = "authorization_code",
		        code = authorizationCode
            };

            AccessToken? accessToken = 
                await Application.NylasClient.ExecutePost<AccessToken?>(Application.ClientSecretBase64, 
                    NylasClient.AuthMethod.BEARER, 
                    tokenUrl, 
                    authCodeRequest);

            return accessToken;
        }
    }
}