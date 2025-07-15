using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.Dto;

namespace NZWalks.UI.Controllers
{
	public class WalksController(IHttpClientFactory httpClientFactory) : Controller
	{
		private HttpClient _client = httpClientFactory.CreateClient();

		public async Task<IActionResult> Index()
		{
			HttpResponseMessage walks = await _client.GetAsync("http://localhost:5112/api/walks");

			walks.EnsureSuccessStatusCode();

			var walksDto = walks.Content.ReadFromJsonAsync<IEnumerable<WalksDto>>();

			return View(walksDto.Result);
		}


		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Add(WalkViewModel model)
		{
			try
			{
				HttpRequestMessage request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("http://localhost:5112/api/walks"),
					Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
				};

				HttpResponseMessage walks = await _client.SendAsync(request);

				walks.EnsureSuccessStatusCode();

				var walksDto = walks.Content.ReadFromJsonAsync<IEnumerable<WalksDto>>();

				if (walksDto is not null)
					return RedirectToAction("Index", "walks");
			}

			catch (Exception ex)
			{
				throw;
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Update(WalksDto model)
		{
			var walkViewModel = new WalkViewModel
			{
				Name = model.Name,
				Description = model.Description,
				Length = model.Length,
				DifficultyId = model.Difficulty.Id,
				RegionId = model.Region.Id,
				WalkImageUrl = model.WalkImageUrl,
			};

			HttpRequestMessage request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"http://localhost:5112/api/walks/{model.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(walkViewModel), Encoding.UTF8, "application/json")
			};

			var httpResponseMessage = await _client.SendAsync(request);

			httpResponseMessage.EnsureSuccessStatusCode();

			var response = httpResponseMessage.Content.ReadFromJsonAsync<WalksDto>();

			if (response is not null)
				return RedirectToAction("Index", "Walks");
			else
				return View(null);

		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var httpResponse = await _client.GetFromJsonAsync<WalksDto>(new Uri($"http://localhost:5112/api/walks/{id}"));

			if (httpResponse is not null)
				return View(httpResponse);

			return View(null);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri($"http://localhost:5112/api/walks/{id}")
			};

			var response = await _client.SendAsync(requestMessage);

			if (response.IsSuccessStatusCode)
				return RedirectToAction("Index", "Walks");
			else
				return View(null);

		}
	}
}
