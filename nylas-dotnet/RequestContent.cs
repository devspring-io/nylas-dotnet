using System.Text;
using System.Text.Json;

namespace Nylas
{
    public abstract class RequestContent
    {
        /// <summary>
        /// Helper method to convert a child object to a properly formatted StringContent payload string.
        /// </summary>
        /// <returns></returns>
        public StringContent ToStringContent()
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonString = JsonSerializer.Serialize<object>(this, options);
            var contentString = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return contentString;
        }
    }
}