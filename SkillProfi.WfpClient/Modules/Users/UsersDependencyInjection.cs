using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Users.View;

namespace SkillProfi.WfpClient.Modules.Users;

public static class UsersDependencyInjection
{
	public static IServiceCollection AddUsersModule(this IServiceCollection services)
	{
		services.AddSingleton<UsersViewModel>();
		
		return services;
	}
}
