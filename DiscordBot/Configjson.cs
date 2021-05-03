using Newtonsoft.Json;

namespace Discord_Bot
{
    struct Configjson
    {
        [JsonProperty ("token")]
        public string Token { get; private set; }
        [JsonProperty ("prefix")]
        public string Prefix { get; private set; }
    }
}
