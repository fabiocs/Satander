using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TechAssessmentSantander.Entity.HackerNews
{
    public class BestStories
    {
        [JsonPropertyName("Ids")]
        public List<int> Ids { get; } = new List<int>();
    }
}
