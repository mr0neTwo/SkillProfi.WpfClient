using SkillProfi.WfpClient.Modules.Auth.Models;
using SkillProfi.WfpClient.Services.Client;

namespace SkillProfi.WfpClient.Modules.Auth;

public sealed class AuthApi(IClient client)
{
	public async Task<AuthResponse?> LoginAsync(UserLoginDto userLoginDto)
	{
		AuthResponse? authResponse = await client.PostAsync<AuthResponse, UserLoginDto>(userLoginDto, "api/Auth/Login");

		return authResponse;
	}

	public async Task<AuthResponse?> CheckAuth()
	{
		return await client.GetAsync<AuthResponse>("api/Auth/Refresh");
	}

	public async Task Logout()
	{
		await client.GetAsync<Type>("api/Auth/Logout");
	}
}