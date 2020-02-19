using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Fourplaces.Services
{
    class ApiClient
    {
		private readonly HttpClient _client = new HttpClient();

		public async Task<HttpResponseMessage> Execute(HttpMethod method, string url, object data = null, string token = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(method, url);
			if (data != null)
			{
				request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
			}
			if (token != null)
			{
				request.Headers.Add("Authorization", $"Bearer {token}");
			}
			return await _client.SendAsync(request);
		}
		public async Task<T> ReadFromResponse<T>(HttpResponseMessage response)
		{
			string result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(result);
		}

		public async Task<HttpResponseMessage> UploadImage(string imageSource)
		{
			HttpClient client = new HttpClient();
			byte[] imageData = File.ReadAllBytes(imageSource);

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

			MultipartFormDataContent requestContent = new MultipartFormDataContent();

			var imageContent = new ByteArrayContent(imageData);
			imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

			// Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
			requestContent.Add(imageContent, "file", "file.jpg");

			request.Content = requestContent;

			HttpResponseMessage response = await client.SendAsync(request);

			return response;
		}
	}
}
