using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Services.View;

namespace SkillProfi.WfpClient.Modules.Services;

public static class ServicesDependencyInjection
{
	public static IServiceCollection AddServicesModule(this IServiceCollection services)
	{
		services.AddSingleton<ServicesViewModel>();
		
		return services;
	}
}
