namespace SkillProfi.WfpClient.Modules.ClientRequests.Models;

public sealed class GetClientRequestListResponse 
{
	public List<ClientRequest> ClientRequests { get; set; }
	public int PageNumber { get; set; }
	public int Count { get; set; }
	public int TotalPages { get; set; }
}
