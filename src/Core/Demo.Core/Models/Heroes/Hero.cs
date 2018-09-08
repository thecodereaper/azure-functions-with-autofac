using Newtonsoft.Json;

namespace Demo.Core.Models.Heroes
{
    public sealed class Hero : IModel
    {
        public Hero(string id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string Version { get; set; }

        internal void ChangeName(string name)
        {
            Name = name;
        }
    }
}