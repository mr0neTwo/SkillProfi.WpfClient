using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Blog.Models;

public sealed class Post
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = string.Empty;
	
	public string FullImageUrl => string.IsNullOrEmpty(ImageUrl)
		? AppVariables.DefaultImagePath
		: $"{AppVariables.BaseUrl}/{ImageUrl}"; 
}