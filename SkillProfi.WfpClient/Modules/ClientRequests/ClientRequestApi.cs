using SkillProfi.WfpClient.Common.Client;
using SkillProfi.WfpClient.Modules.ClientRequests.Models;

namespace SkillProfi.WfpClient.Modules.ClientRequests;

public sealed class ClientRequestApi(IClient client)
{
	public async Task<GetClientRequestListResponse?> GetClientRequests(GetClientApiRequestListApiRequest request)
	{
		GetClientRequestListResponse? response = await client.GetAsync<GetClientRequestListResponse, GetClientApiRequestListApiRequest>(request, "/api/ClientRequest/GetList");
		
		return response;
	}

	public async Task UpdateClientRequest(ClientRequest clientRequests)
	{
		UpdateClientRequestDto dto = new()
		{
			Id = clientRequests.Id, 
			Status = clientRequests.Status,
		};

		await client.PutAsync(dto, "/api/ClientRequest/Update");
	}
}