
using Nylas;

namespace nylas_dotnet
{
    public class NylasAccount
    {
        public NylasClient Client { get; private set; }
        public string AccessToken { get; private set; }

        public NylasAccount(NylasClient client, string accessToken)
        {
            this.Client = client;
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Calls endpoint /account
        /// https://developer.nylas.com/docs/api/v2/#get/account
        /// </summary>
        /// <returns></returns>
        public async Task<AccountDetail?> FetchAccountByAccessToken()
        {
            Uri url = Client.CreateUrl("account");
            return await Client.ExecuteGet<AccountDetail>(AccessToken, NylasClient.AuthMethod.BEARER, url);
        }


        /// <summary>
        /// Calls endpoint /oauth/revoke
        /// See https://developer.nylas.com/docs/api/v2/#post/oauth/revoke
        /// </summary>
        public async Task<bool> RevokeAccessToken()
        {
            Uri url = Client.CreateUrl("oauth/revoke");
            SuccessResponse? response =
                await Client.ExecutePost<SuccessResponse?>(AccessToken, NylasClient.AuthMethod.BEARER, url, null);
            return response != null && response.Success;
        }
    }
}
