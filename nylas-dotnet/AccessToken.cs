namespace Nylas
{
    /// <summary>
    /// Defines the response in the authentication code flow when exchanging a code for an access token.
    /// </summary>
    public class AccessToken
    {
        public string? access_token { get; set; }
        public string? account_id { get; set; }
        public string? email_address { get; set; }
        public string? provider { get; set; }

        public override string ToString()
        {
            return $"AccessToken [access_token={access_token}, account_id={account_id}, email_address={email_address}, provider={provider}]";
        }
    }
}