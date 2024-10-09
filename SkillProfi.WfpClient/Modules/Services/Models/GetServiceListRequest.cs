using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Services.Models;

public sealed class GetServiceListRequest : IApiRequest
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}
