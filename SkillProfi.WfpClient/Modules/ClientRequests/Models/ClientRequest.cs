namespace SkillProfi.WfpClient.Modules.ClientRequests.Models;

public sealed class ClientRequest
{
	public int Id { get; set; }
	public long CreationDate { get; set; }
	public string ClientName { get; set; } = string.Empty;
	public string ClientEmail { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public ClientRequestStatus Status { get; set; }
}