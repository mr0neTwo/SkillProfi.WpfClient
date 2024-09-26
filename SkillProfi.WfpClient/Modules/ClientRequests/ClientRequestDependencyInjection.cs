using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.ClientRequests.View;

namespace SkillProfi.WfpClient.Modules.ClientRequests;

public static class ClientRequestDependencyInjection
{
	public static IServiceCollection AddClientRequestModule(this IServiceCollection services)
	{
		services.AddSingleton<ClientRequestViewModel>();
		
		return services;
	}
}
