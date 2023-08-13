using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NZWalksUI.Models;
using NZWalksUI.Models.DTO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace NZWalksUI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IHttpClientFactory httpClientFactory, ILogger<RegionsController> logger) 
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                //get all regions from API

                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7216/api/Regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                //handle the exception
            }
            return View(response);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7216/api/Regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception)
            {
                //exception handling
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response =  await client.GetFromJsonAsync<RegionDto>($"https://localhost:7216/api/Regions/{id.ToString()}");

            if(response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
       
             var client = httpClientFactory.CreateClient();

             var httpRequestMessage = new HttpRequestMessage()
             {
                 Method = HttpMethod.Put,
                 RequestUri = new Uri($"https://localhost:7216/api/Regions/{request.Id}"),
                 Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
             };
           
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            
            System.Diagnostics.Debug.WriteLine($"============>{httpResponseMessage}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
                    
                if (response is not null)
                {
                    return RedirectToAction("Edit", "Regions");
                }
          

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7216/api/Regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {
                //handle exception
            }
            return View("Edit");
        }
    }
}
