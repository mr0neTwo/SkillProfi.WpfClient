namespace SkillProfi.WfpClient.Modules.Services.Models;

public sealed class UpdateServiceDto 
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}