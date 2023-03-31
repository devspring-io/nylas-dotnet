using System.Text;

namespace Nylas
{
    public class NylasApplication
    {
        public NylasClient NylasClient { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string ClientSecretBase64 { get; private set; }

        public NylasApplication(NylasClient? nylasClient, string? clientId, string? clientSecret)
        {
            // if any of the input parameters are null then throw an ArgumentNullException exception
            if (nylasClient == null || clientId == null || clientSecret == null)
            {
                throw new ArgumentNullException("NylasApplication constructor parameters cannot be null");
            }

            NylasClient = nylasClient;
            ClientId = clientId;
            ClientSecret = clientSecret;

            byte[] bytes = Encoding.UTF8.GetBytes(ClientSecret);

            ClientSecretBase64 = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        /// <summary>
        /// Returns a new instance of the HostedAuthentication class.
        /// </summary>
        /// <returns></returns>
        public HostedAuthentication HostedAuthentication()
        {
            return new HostedAuthentication(this);
        }
    }
}