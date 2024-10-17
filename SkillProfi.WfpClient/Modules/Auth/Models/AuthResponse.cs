using SkillProfi.WfpClient.Modules.Users.Models;

namespace SkillProfi.WfpClient.Modules.Auth.Models;

public sealed class AuthResponse
{
	public User User { get; set; }
	public string Token { get; set; }
	public string ErrorMessage { get; set; }
	public bool Success { get; set; }
}