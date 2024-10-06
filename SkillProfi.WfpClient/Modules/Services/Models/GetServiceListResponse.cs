using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Services.Models;

public sealed class GetServiceListResponse : ApiResponseBody
{
	public List<Service> Services { get; set; }
	public int PageNumber { get; set; }
	public int Count { get; set; }
	public int TotalPages { get; set; }
}
