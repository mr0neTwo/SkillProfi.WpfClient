using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Users.View;

namespace SkillProfi.WfpClient.Modules.Users;

public static class UsersDependencyInjection
{
	public static IServiceCollection AddUsersModule(this IServiceCollection services)
	{
		services.AddSingleton<UsersApi>();
		services.AddSingleton<UsersViewModel>();
		services.AddSingleton<UserEditorViewModel>();
		
		return services;
	}
}
