using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Layout.View;

namespace SkillProfi.WfpClient.Modules.Layout;

public static class LayoutDependencyInjection
{
	public static IServiceCollection AddLayoutModule(this IServiceCollection services)
	{
		services.AddSingleton<LayoutViewModel>();

		services.AddSingleton<RootWindow>
		(
			provider => new RootWindow()
			{
				DataContext = provider.GetRequiredService<RootViewModel>()
			}
		);
		
		services.AddSingleton<RootViewModel>();

		return services;
	}
}
