using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Client;

namespace SkillProfi.WfpClient.Modules.Services;

public sealed class ServicesApi(IClient client)
{
	public async Task<GetServiceListResponse?> GetList(GetServiceListRequest request)
	{
		string url = "api/Service/GetList";
		GetServiceListResponse? response = await client.GetAsync<GetServiceListResponse, GetServiceListRequest>(request, url);
		
		return response;
	}

	public async Task Create(CreateServiceDto createServiceDto)
	{
		string url = "api/Service/Create";
		await client.PostVoidAsync(createServiceDto, url);
	}

	public async Task Update(UpdateServiceDto updateServiceDto)
	{
		string url = "api/Service/Update";
		await client.PutAsync(updateServiceDto, url);
	}

	public async Task Delete(int serviceId)
	{
		string url = "api/Service/Delete";
		await client.DeleteAsync(serviceId, url);
	}
}
