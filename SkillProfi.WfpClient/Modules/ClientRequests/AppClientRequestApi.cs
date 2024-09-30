using SkillProfi.WfpClient.Modules.ClientRequests.Models;
using SkillProfi.WfpClient.Services.Client;

namespace SkillProfi.WfpClient.Modules.ClientRequests;

public sealed class AppClientRequestApi(IClient client)
{
	public async Task<GetClientRequestListResponse?> GetClientRequests(GetClientApiRequestListApiRequest request)
	{
		GetClientRequestListResponse? response = await client.GetAsync<GetClientRequestListResponse, GetClientApiRequestListApiRequest>(request, "/api/ClientRequest/GetList");
		
		return response;
	}
}	
