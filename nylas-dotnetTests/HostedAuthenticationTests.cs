using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nylas.Tests
{
    /// <summary>
    /// Unit tests for the HostedAuthentication class
    /// These test(s) may call Nylas APIs and require valid Nylas application credentials.
    /// NOTE: Some or all test cases require a client id and client secret for a Nylas
    /// application. These must be store in a user secrets file, secrets.json, for the
    /// nylas-dotnetTests project. The expected content of the secrets file is:
    /// {
    ///     "client_id": "your_client_id",
    ///     "client_secret": "your_client_secret",
    ///     "code": "your_code"
    /// }
    /// </summary>
    [TestClass()]
    public class HostedAuthenticationTests
    {
        NylasClient? _client = null;
        private string? _clientId;
        private string? _clientSecret;
        private string? _code;

        [TestInitialize()]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<HostedAuthenticationTests>()
                .Build();

            _clientId = config["client_id"];
            _clientSecret = config["client_secret"];
            _code = config["code"];

            _client = new NylasClient();
        }

        /// <summary>
        /// This test case was created to test the HostedAuthentication.FetchToken method, specifically
        /// the exchange of a code for an access token from the Nylas API. This test case requires
        /// some intermediate steps...the Nylas CLI is good about handling the full authentication flow...
        /// but additional work will need to be done to enable a live test of this code path. Likely not a
        /// candidate for unit testing. Thus, the test case is disabled via the [TestMethod()] attribute.
        /// </summary>
        //[TestMethod()]
        public void FetchTokenTest()
        {
            NylasApplication application = new NylasApplication(_client, _clientId, _clientSecret);
            AccessToken? token = application.HostedAuthentication().FetchToken(_code).Result;
            Assert.IsNotNull(token);
        }
    }
}