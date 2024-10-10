namespace SkillProfi.WfpClient.Modules.Blog.Models;

public sealed class GetPostListResponse
{
	public List<Post> Posts { get; set; }
	public int Count { get; set; }
	public int PageNumber { get; set; }
	public int TotalPages { get; set; }
}
