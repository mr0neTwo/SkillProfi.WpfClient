namespace SkillProfi.WfpClient.Modules.Projects.Models;

public sealed class GetProjectListResponse
{
	public List<Project> Projects { get; set; }
	public int Count { get; set; }
	public int PageNumber { get; set; }
	public int TotalPages { get; set; }
}
