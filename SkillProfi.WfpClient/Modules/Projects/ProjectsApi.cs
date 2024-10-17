using SkillProfi.WfpClient.Common.Client;
using SkillProfi.WfpClient.Modules.Projects.Models;

namespace SkillProfi.WfpClient.Modules.Projects;

public sealed class ProjectsApi(IClient client)
{
	public async Task<GetProjectListResponse?> GetList(GetProjectListRequest request)
	{
		string url = "api/Project/GetList";
		GetProjectListResponse? response = await client.GetAsync<GetProjectListResponse, GetProjectListRequest>(request, url);
		
		return response;
	}

	public async Task Create(CreateProjectDto createProjectDto)
	{
		string url = "api/Project/Create";
		await client.PostVoidAsync(createProjectDto, url);
	}

	public async Task Update(UpdateProjectDto updateProjectDto)
	{
		string url = "api/Project/Update";
		await client.PutAsync(updateProjectDto, url);
	}

	public async Task Delete(int projectId)
	{
		string url = "api/Project/Delete";
		await client.DeleteAsync(projectId, url);
	}
}
