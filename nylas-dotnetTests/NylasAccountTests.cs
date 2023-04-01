using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nylas;
using Nylas.Tests;

namespace nylas_dotnet.Tests
{
    [TestClass()]
    public class NylasAccountTests
    {
        private NylasClient? _client = null;
        private string? _clientId;
        private string? _clientSecret;
        private string? _accessToken;

        [TestInitialize]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<HostedAuthenticationTests>()
                .Build();

            _clientId = config["client_id"];
            _clientSecret = config["client_secret"];
            _accessToken = config["access_token"];

            _client = new NylasClient();
        }

        [TestMethod()]
        public void FetchAccountByAccessTokenTest()
        {
            // if any members are null then assert fail
            if (_client == null || _clientId == null || _clientSecret == null || _accessToken == null)
            {
                Assert.Fail();
            }

            NylasAccount account = new NylasAccount(_client, _accessToken);
            Assert.IsNotNull(account);

            var details = account.FetchAccountByAccessToken().Result;
            Assert.IsNotNull(details);
        }
    }
}