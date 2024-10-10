namespace SkillProfi.WfpClient.Modules.ClientRequests.Models;

public sealed class GetClientApiRequestListApiRequest
{
	public long StartTimestamp { get; set; }
	public long EndTimeStamp { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}