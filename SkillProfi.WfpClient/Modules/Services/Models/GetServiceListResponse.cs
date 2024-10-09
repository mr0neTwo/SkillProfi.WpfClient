using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Services.Models;

public sealed class GetServiceListResponse : IApiResponse
{
	public List<Service> Services { get; set; }
	public int PageNumber { get; set; }
	public int Count { get; set; }
	public int TotalPages { get; set; }
}
