using System.Net.Http;
using System.Text;
using Flurl;
using Newtonsoft.Json;
using SkillProfi.WfpClient.Common;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace SkillProfi.WfpClient.Services.Client;

public sealed class AppClient : IClient
{
	private HttpClient Client { get; set; }

	public AppClient()
	{
		Client = new HttpClient();
		Client.BaseAddress = new Uri("http://localhost:5272/");
	}

	public async Task<TResponseBody?> GetAsync<TResponseBody, TRequest>(TRequest request, string url)
		where TResponseBody : ApiResponseBody
		where TRequest : ApiRequest
	{
		Url requestUri = url.SetQueryParams(request);

		return await GetAsync<TResponseBody>(requestUri);
	}
	
	public async Task<TResponseBody?> GetAsync<TResponseBody>(string url)
		where TResponseBody : ApiResponseBody
	{
		HttpResponseMessage response = await Client.GetAsync(url);

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}
		
		string responseBody = await response.Content.ReadAsStringAsync();
		TResponseBody? body = JsonConvert.DeserializeObject<TResponseBody>(responseBody);

		return body;
	}

	public async Task<TResponseBody?> PostAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
		where TResponseBody : ApiResponseBody
		where TRequest : ApiRequest
	{
		string jsonContent = JsonSerializer.Serialize(apiRequest);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		HttpResponseMessage response = await Client.PostAsync(url, content);

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}
		
		string responseBody = await response.Content.ReadAsStringAsync();
		TResponseBody? body = JsonConvert.DeserializeObject<TResponseBody>(responseBody);

		return body;
	}

	public async Task PutAsync<TRequest>(TRequest apiRequest, string url) 
		where TRequest : ApiRequest
	{
		string jsonContent = JsonSerializer.Serialize(apiRequest);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		await Client.PutAsync(url, content);
	}
}