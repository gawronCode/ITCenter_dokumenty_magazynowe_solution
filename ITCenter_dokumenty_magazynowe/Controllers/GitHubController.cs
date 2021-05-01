using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.ApiModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace ITCenter_dokumenty_magazynowe.Controllers
{
    public class GitHubController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.github.com/repos/gawronCode/ITCenter_dokumenty_magazynowe_solution");
            var headerValue = new ProductInfoHeaderValue("WarehouseApp", "1.0");
            request.Headers.UserAgent.Add(headerValue);
            var response = await httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);

            var model = new GitHubRepoData
            {
                CreationDate = json["created_at"].ToString(),
                Description = json["description"].ToString(),
                Id = json["id"].ToString(),
                Name = json["name"].ToString(),
                OwnerName = json["owner"]["login"].ToString(),
                SubscribersCount = json["subscribers_count"].ToString(),
            };

            return View(model);
        }
    }
}
