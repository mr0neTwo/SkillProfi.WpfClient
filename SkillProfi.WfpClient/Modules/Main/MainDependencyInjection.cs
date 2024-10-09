using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Main.View;

namespace SkillProfi.WfpClient.Modules.Main;

public static class MainDependencyInjection
{
	public static IServiceCollection AddMainModule(this IServiceCollection services)
	{
		services.AddSingleton<SiteItemApi>();
		services.AddSingleton<MainViewModel>();
		
		return services;
	}
}
