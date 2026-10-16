using Newtonsoft.Json;

namespace Git.Training.Model.Response
{
    public class TemplateResponseDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
