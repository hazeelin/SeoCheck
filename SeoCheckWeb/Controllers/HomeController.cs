using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeoCheckWeb.Models;
using SeoCheckWeb.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index(string url, string keywords)
        {

            if (url == null || keywords == null)
            {
                return View();
            }

            var results = new RankedUrlViewModel();
            var apiEndPoint = StaticDetails.ApiUrl + "/" + url + "/" + keywords;

            using (var httpClient = _clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync(apiEndPoint))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<RankedUrlViewModel>(apiResponse);
                }
            }
            return View(results);



        }
    }
}
