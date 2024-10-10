namespace SkillProfi.WfpClient.Modules.ClientRequests.Models;

public sealed class UpdateClientRequestDto 
{
	public int Id { get; set; }
	public ClientRequestStatus Status { get; set; }
}
