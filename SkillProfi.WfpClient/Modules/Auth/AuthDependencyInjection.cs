using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Auth.View;

namespace SkillProfi.WfpClient.Modules.Auth;

public static class AuthDependencyInjection
{
	public static IServiceCollection AddAuthModule(this IServiceCollection services)
	{
		services.AddSingleton<AuthApi>();
		services.AddSingleton<AuthViewModel>();
		
		return services;
	}
}
