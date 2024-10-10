namespace SkillProfi.WfpClient.Modules.Blog.Models;

public sealed class UpdatePostDto
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? ImageBase64 { get; set; }
}
