using EnergyCompanyConnector.cs.Models.Requests;
using EnergyCompanyConnector.cs.Models.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyCompanyConnector.cs.Clients.Customers
{
    public class MeterReadingsClient
    {
        static readonly HttpClient s_client = new HttpClient();

        public async Task<PostMeterReadingsResponse> Upload(PostMeterReadingsRequest request, CancellationToken cancellationToken = default)
        {
            var requestUrl = "https://localhost:44362/customers/meter-readings";
            var responseMessage = await s_client.PostAsJsonAsync(requestUrl, request, cancellationToken);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PostMeterReadingsResponse>(jsonString);
        }
    }
}
