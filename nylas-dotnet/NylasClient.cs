namespace Nylas
{
    public class NylasClient
    {
        public static readonly string DEFAULT_BASE_URL = "https://api.nylas.com/";
	    public static readonly string EU_BASE_URL = "https://ireland.api.nylas.com/";

        public Uri BaseUrl { get; private set; }
        public HttpClient HttpClient { get; private set; } = new HttpClient();

        public NylasClient() : this(DEFAULT_BASE_URL)
        {
        }
        
        public NylasClient(String baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        /// <summary>
        /// Returns a new instance of the NylasApplication class.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public NylasApplication Application(String clientId, String clientSecret)
        {
            return new NylasApplication(this, clientId, clientSecret);
        }

        /// <summary>
        /// Generates a full URL from the base URL appended with the path segments.
        /// </summary>
        /// <param name="pathSegments"></param>
        /// <returns></returns>
        internal Uri CreateUrl(string pathSegments)
        {
            return new Uri(BaseUrl, pathSegments);
        }
    }
}