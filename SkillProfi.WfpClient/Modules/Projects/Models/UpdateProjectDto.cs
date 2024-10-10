namespace SkillProfi.WfpClient.Modules.Projects.Models;

public sealed class UpdateProjectDto
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? ImageBase64 { get; set; }
}
