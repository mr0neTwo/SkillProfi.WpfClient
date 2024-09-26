using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Contacts.View;

namespace SkillProfi.WfpClient.Modules.Contacts;

public static class ContactsDependencyInjection
{
	public static IServiceCollection AddContactsModule(this IServiceCollection services)
	{
		services.AddSingleton<ContactsViewModel>();
		
		return services;
	}
}
