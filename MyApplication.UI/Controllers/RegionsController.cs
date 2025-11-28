using Microsoft.AspNetCore.Mvc;
using MyApplication.UI.Models;
using MyApplication.UI.Models.DTOs;
using System.Threading.Tasks;

namespace MyApplication.UI.Controllers;

public class RegionsController : Controller
{
   private readonly IHttpClientFactory _httpClientFactory;

   public RegionsController(IHttpClientFactory httpClientFactory)
   {
      this._httpClientFactory = httpClientFactory;
   }

   [HttpGet]
   public async Task<IActionResult> Index()
   {
      try
      {
         var client = _httpClientFactory.CreateClient();

         var httpResponseMessage = await client.GetAsync("https://localhost:7170/api/regions");

         httpResponseMessage.EnsureSuccessStatusCode();

         var response = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();

         return View(response);
      }
      catch
      {
         return View("Error");
      }

   }

   [HttpGet]
   public IActionResult Add()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Add(AddRegionViewModel request)
   {
      try
      {
         var client = _httpClientFactory.CreateClient();

         var httpRequestMessage = new HttpRequestMessage()
         {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://localhost:7170/api/regions"),
            Content = JsonContent.Create(new
            {
               Code = request.Code,
               Name = request.Name,
               RegionImageUrl = request.RegionImageUrl
            })
         };

         var httpResponseMessage = await client.SendAsync(httpRequestMessage);

         httpResponseMessage.EnsureSuccessStatusCode();

         var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

         if (response is not null)
         {
            return RedirectToAction("Index");
         }

         return View();
      }
      catch
      {
         return View("Error");
      }
   }

   [HttpGet]
   public async Task<IActionResult> Edit(Guid id)
   {
      try
      {
         var client = _httpClientFactory.CreateClient();

         var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7170/api/regions/{id.ToString()}");

         if (response is not null)
         {
            return View(response);
         }

         return View("Error");
      }
      catch
      {
         return View("Error");
      }
   }

   [HttpPost]
   public async Task<IActionResult> Edit(RegionDto request)
   {
      try
      {
         var client = _httpClientFactory.CreateClient();

         var httpRequestMessage = new HttpRequestMessage()
         {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"https://localhost:7170/api/regions/{request.Id.ToString()}"),
            Content = JsonContent.Create(new
            {
               Id = request.Id,
               Code = request.Code,
               Name = request.Name,
               RegionImageUrl = request.RegionImageUrl
            })
         };

         var httpResponseMessage = await client.SendAsync(httpRequestMessage);

         httpResponseMessage.EnsureSuccessStatusCode();

         var response = httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

         if (response is not null)
         {
            return RedirectToAction(nameof(Edit));
         }
      }
      catch
      {

      }

      return View();
   }


   [HttpPost]
   public async Task<IActionResult> Delete(RegionDto dto)
   {
      try
      {
         var client = _httpClientFactory.CreateClient();

         var httpRequestMessage = new HttpRequestMessage()
         {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"https://localhost:7170/api/regions/{dto.Id.ToString()}")
         };

         var httpResponseMessage = await client.SendAsync(httpRequestMessage);

         httpResponseMessage.EnsureSuccessStatusCode();

         var response = httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

         if (response is not null)
         {
            return RedirectToAction(nameof(Index));
         }
         return View("Error");
      }
      catch
      {
         return View("Error");
      }

   }

}
