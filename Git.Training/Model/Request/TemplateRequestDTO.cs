using Newtonsoft.Json;
using Git.Training.Model.ExampleDB;

namespace Git.Training.Model.Request
{
    public class AddTemplateRequestDTO
    {
        [JsonProperty("data")]
        public Template Data { get; set; }
    }
    public class UpdateTemplateRequestDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("data")]
        public Template Data { get; set; }
    }
    public class DeleteTemplateRequestDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
	public class ExecUpdateTemplateRequestDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("data")]
        public Template Data { get; set; }
    }

    public class ExecDeleteTemplateRequestDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
