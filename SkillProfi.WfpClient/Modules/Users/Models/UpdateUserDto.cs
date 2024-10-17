namespace SkillProfi.WfpClient.Modules.Users.Models;

public sealed class UpdateUserDto 
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
}
