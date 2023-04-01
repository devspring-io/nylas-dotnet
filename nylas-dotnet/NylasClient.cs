using System.Net.Http.Headers;
using System.Text.Json;

namespace Nylas
{
    public class NylasClient
    {
        public static readonly string DEFAULT_BASE_URL = "https://api.nylas.com/";
	    public static readonly string EU_BASE_URL = "https://ireland.api.nylas.com/";

        public enum AuthMethod { BASIC, BASIC_WITH_CREDENTIALS, BEARER }
        public enum HttpMethod { GET, PUT, POST, DELETE, PATCH }

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

        /// <summary>
        /// Sends a POST request to the specified URL and attempts to deserialize the response as a DTO.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="authUser"></param>
        /// <param name="authMethod"></param>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<T?> ExecutePost<T>(string authUser, 
            AuthMethod authMethod, 
            Uri url,
            RequestContent? body)
        {
            var payload = body != null ? body.ToStringContent() : new StringContent(string.Empty);

            SetHttpClientHeaders(authUser, authMethod);

            var response = await this.HttpClient.PostAsync(url, payload);
            
            return await DeserializeResponse<T>(response);
        }

        /// <summary>
        /// Sends a GET request to the specified URL and attempts to deserialize the response as a DTO.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="authUser"></param>
        /// <param name="authMethod"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T?> ExecuteGet<T>(string authUser,
            AuthMethod authMethod,
            Uri url)
        {
            SetHttpClientHeaders(authUser, authMethod);

            var response = await this.HttpClient.GetAsync(url);

            return await DeserializeResponse<T>(response);
        }

        /// <summary>
        /// Sets the common request headers
        /// TODO: This assumes that the Content-Type is application/json.
        /// TODO: This only supports Bearer authentication.
        /// </summary>
        /// <param name="authUser"></param>
        /// <param name="authMethod"></param>
        private void SetHttpClientHeaders(string authUser, AuthMethod authMethod)
        {
            var httpClient = this.HttpClient;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization =
                authMethod switch
                {
                    AuthMethod.BEARER => new AuthenticationHeaderValue("Bearer", authUser),
                    _ => null
                };
        }

        /// <summary>
        /// Attempts to deserialize a HttpResponseMessage as a DTO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
        {
            T? result = default(T);

            if (response.IsSuccessStatusCode)
            {
                result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }

            return result;
        }
    }
}