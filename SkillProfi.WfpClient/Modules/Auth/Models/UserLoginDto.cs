using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Auth.Models;

public sealed class UserLoginDto : ApiRequest
{
	public string Email { get; set; }
	public string Password { get; set; }
}
