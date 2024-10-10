namespace SkillProfi.WfpClient.Modules.Projects.Models;

public sealed class CreateProjectDto 
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }
}
