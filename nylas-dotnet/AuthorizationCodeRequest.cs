namespace Nylas
{
    /// <summary>
    /// DTO used in the Authorization Code flow for exchanging the code (after OAuth is completed) for
    /// a permanent access token from Nylas.
    /// </summary>
    public class AuthorizationCodeRequest : RequestContent
    {
        public string client_id { get; set; } = string.Empty;
        public string grant_type { get; set; } = string.Empty;
        public string client_secret { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
    }
}