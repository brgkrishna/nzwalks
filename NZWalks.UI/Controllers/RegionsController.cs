using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.Dto;

namespace NZWalks.UI.Controllers
{
	public class RegionsController : Controller
	{
		private readonly HttpClient _httpClient ;

		public RegionsController(IHttpClientFactory client)
		{
			this._httpClient = client.CreateClient();
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<RegionDto> response= new List<RegionDto>();

			try
			{
				var httpResponseMessage = await _httpClient.GetAsync("http://localhost:5112/api/regions");

				httpResponseMessage.EnsureSuccessStatusCode();

				response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

				ViewBag.Response = response;

			}
			catch (Exception)
			{

				throw;
			}

			//GetAllRegionsfromAPI
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
				var httpRequestMessage = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("http://localhost:5112/api/regions"),
					Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
				};

				var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

				if (response is not null)
					RedirectToAction("Index", "Regions");

			}
			catch (Exception)
			{

				throw;
			}

			//GetAllRegionsfromAPI
			return View();
		}
	}
}
