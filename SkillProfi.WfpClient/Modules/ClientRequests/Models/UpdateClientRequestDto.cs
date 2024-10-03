using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.ClientRequests.Models;

public sealed class UpdateClientRequestDto : ApiRequest
{
	public int Id { get; set; }
	public ClientRequestStatus Status { get; set; }
}
