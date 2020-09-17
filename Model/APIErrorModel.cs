using Newtonsoft.Json;

namespace Notification.Model
{
    public class APIErrorModel
    {
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorDetails")]
        public string ErrorDetails { get; set; }

        public bool ShouldSerializeErrorDetails()
        {
            if (string.IsNullOrEmpty(ErrorDetails))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
