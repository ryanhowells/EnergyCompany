using Newtonsoft.Json;

namespace EnergyCompanyConnector.cs.Models.Responses
{
    public class PostMeterReadingsResponse
    {
        [JsonProperty] 
        public int TotalSuccessCount { get; set; }

        [JsonProperty]
        public int TotalFailCount { get; set; }
    }
}
