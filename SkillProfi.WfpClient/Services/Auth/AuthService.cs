using SkillProfi.WfpClient.Modules.Auth;
using SkillProfi.WfpClient.Modules.Auth.Models;
using SkillProfi.WfpClient.Modules.Users.Models;

namespace SkillProfi.WfpClient.Services.Auth;

public sealed class AuthService : IAuthService
{
	public event Action<bool>? LoginStatusChanged;
	public User? User { get; private set; }
	
	public bool LoggedIn 
	{ 
		get => _loggedIn;
		private set
		{
			if (_loggedIn == value)
			{
				return;
			}

			_loggedIn = value;
			LoginStatusChanged?.Invoke(value);
		} 
	}

	private bool _loggedIn;
	private readonly AuthApi _authApi;

	public AuthService(AuthApi authApi)
	{
		_authApi = authApi;
		_ = InitializeAsync(); 
	}


	public async Task<AuthResponse?> LoginAsync(UserLoginDto userLoginDto)
	{
		AuthResponse? authResponse = await _authApi.LoginAsync(userLoginDto);

		if (authResponse != null && authResponse.Success)
		{
			LoggedIn = true;
			User = authResponse.User;
		}
		else
		{
			LoggedIn = false;
			User = null;
		}
		
		return authResponse;
	}

	public async Task<bool> CheckAuth()
	{
		AuthResponse? authResponse = await _authApi.CheckAuth();
		
		if (authResponse != null && authResponse.Success)
		{
			LoggedIn = true;
			User = authResponse.User;

			return true;
		}
		
		LoggedIn = false;
		User = null;
		
		return false;
	}

	public async Task LogoutAsync()
	{
		await _authApi.Logout();
	}
	
	private async Task InitializeAsync()
	{
		LoggedIn = await CheckAuth();
	}
}