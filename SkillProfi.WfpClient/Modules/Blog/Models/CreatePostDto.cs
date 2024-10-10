namespace SkillProfi.WfpClient.Modules.Blog.Models;

public sealed class CreatePostDto 
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string? ImageBase64 { get; set; }
}
