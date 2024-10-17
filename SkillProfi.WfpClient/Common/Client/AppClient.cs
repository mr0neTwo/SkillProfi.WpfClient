using System.Net.Http;
using System.Text;
using Flurl;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace SkillProfi.WfpClient.Common.Client;

public sealed class AppClient : IClient
{
	private HttpClient Client { get; set; }

	public AppClient()
	{
		Client = new HttpClient();
		Client.BaseAddress = new Uri(AppVariables.BaseUrl);
	}

	public async Task<TResponseBody?> GetAsync<TResponseBody, TRequest>(TRequest request, string url)
	{
		Url requestUri = url.SetQueryParams(request);

		return await GetAsync<TResponseBody>(requestUri);
	}
	
	public async Task<TResponseBody?> GetAsync<TResponseBody>(string url)
	{
		HttpResponseMessage response = await Client.GetAsync(url);

		if (!response.IsSuccessStatusCode)
		{
			return default;
		}
		
		string responseBody = await response.Content.ReadAsStringAsync();
		TResponseBody? body = JsonConvert.DeserializeObject<TResponseBody>(responseBody);

		return body;
	}

	public async Task<TResponseBody?> PostAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
	{
		string jsonContent = JsonSerializer.Serialize(apiRequest);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		HttpResponseMessage response = await Client.PostAsync(url, content);

		if (!response.IsSuccessStatusCode)
		{
			return default;
		}
		
		string responseBody = await response.Content.ReadAsStringAsync();
		TResponseBody? body = JsonConvert.DeserializeObject<TResponseBody>(responseBody);

		return body;
	}
	
	public async Task PostVoidAsync<TRequest>(TRequest apiRequest, string url)
	{
		string jsonContent = JsonSerializer.Serialize(apiRequest);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		await Client.PostAsync(url, content);
	}

	public async Task PutAsync<TRequest>(TRequest apiRequest, string url) 
	{
		string jsonContent = JsonSerializer.Serialize(apiRequest);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		await Client.PutAsync(url, content);
	}

	public async Task DeleteAsync(int id, string url)
	{
		await Client.DeleteAsync($"{url}/{id}");
	}
}