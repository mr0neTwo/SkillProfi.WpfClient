using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Auth.Models;

public sealed class AuthResponse : ApiResponseBody
{
	public User User { get; set; }
	public string Token { get; set; }
	public string ErrorMessage { get; set; }
	public bool Success { get; set; }
}