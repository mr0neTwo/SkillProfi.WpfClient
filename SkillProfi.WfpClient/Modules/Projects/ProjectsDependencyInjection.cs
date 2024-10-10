using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Projects.View;

namespace SkillProfi.WfpClient.Modules.Projects;

public static class ProjectsDependencyInjection
{
	public static IServiceCollection AddProjectsModule(this IServiceCollection services)
	{
		services.AddSingleton<ProjectsApi>();
		services.AddSingleton<ProjectsViewModel>();
		services.AddSingleton<ProjectEditorViewModel>();
		
		return services;
	}
}
