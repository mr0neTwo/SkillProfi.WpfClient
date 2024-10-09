using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Auth.Models;

public sealed class AuthResponse : IApiResponse
{
	public User User { get; set; }
	public string Token { get; set; }
	public string ErrorMessage { get; set; }
	public bool Success { get; set; }
}