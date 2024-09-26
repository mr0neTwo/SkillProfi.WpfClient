using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Auth.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SkillProfi.WfpClient.Modules.Auth;

public sealed class AuthApi : BaseApi
{
	public async Task<AuthResponse?> LoginAsync(UserLoginDto userLoginDto)
	{
		string jsonContent = JsonSerializer.Serialize(userLoginDto);
		var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
		
		HttpResponseMessage response = await Client.PostAsync("api/Auth/Login", content);
		response.EnsureSuccessStatusCode();
		
		string responseBody = await response.Content.ReadAsStringAsync();
		AuthResponse? authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseBody);

		return authResponse;
	}

	public async Task<AuthResponse?> CheckAuth()
	{
		HttpResponseMessage response = await Client.GetAsync("api/Auth/Refresh");

		response.EnsureSuccessStatusCode();
		
		string responseBody = await response.Content.ReadAsStringAsync();
		AuthResponse? authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody);

		return authResponse;
	}

	public async Task Logout()
	{
		HttpResponseMessage response = await Client.GetAsync("api/Auth/Logout");
	}
}