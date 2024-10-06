using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Client;

namespace SkillProfi.WfpClient.Modules.Services;

public sealed class ServicesApi(IClient client)
{
	public async Task<GetServiceListResponse?> GetList(GetServiceListRequest request)
	{
		GetServiceListResponse? response = await client.GetAsync<GetServiceListResponse, GetServiceListRequest>(request, "api/Service/GetList");
		
		return response;
	}

	public async Task Create(Service service)
	{
		CreateServiceDto createServiceDto = new()
		{
			Title = service.Title, 
			Description = service.Description
		};

		await client.PostVoidAsync(createServiceDto, "api/Service/Create");
	}

	public async Task Update(Service service)
	{
		UpdateServiceDto updateServiceDto = new()
		{
			Id = service.Id, 
			Title = service.Title, 
			Description = service.Description
		};

		await client.PutAsync(updateServiceDto, "api/Service/Update");
	}

	public async Task Delete(int serviceId)
	{
		await client.DeleteAsync(serviceId, "api/Service/Delete");
	}
}
