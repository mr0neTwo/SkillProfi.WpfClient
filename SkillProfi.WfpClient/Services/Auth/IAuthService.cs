using SkillProfi.WfpClient.Modules.Auth.Models;
using SkillProfi.WfpClient.Modules.Users.Models;

namespace SkillProfi.WfpClient.Services.Auth;

public interface IAuthService
{
	public event Action<bool> LoginStatusChanged;
	User? User { get; }
	bool LoggedIn { get; }

	Task<AuthResponse?> LoginAsync(UserLoginDto userLoginDto);

	Task<bool> CheckAuth();
}
