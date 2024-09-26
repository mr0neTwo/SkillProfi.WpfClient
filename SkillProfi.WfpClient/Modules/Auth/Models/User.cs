namespace SkillProfi.WfpClient.Modules.Auth.Models;

public sealed class User
{
	public int Id { get; set; }
	public long CreationDate { get; set; }
	public string? Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
}
