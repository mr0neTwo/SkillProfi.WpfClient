using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.Client;
using SkillProfi.WfpClient.Modules.Auth;
using SkillProfi.WfpClient.Modules.Blog;
using SkillProfi.WfpClient.Modules.ClientRequests;
using SkillProfi.WfpClient.Modules.Contacts;
using SkillProfi.WfpClient.Modules.Layout;
using SkillProfi.WfpClient.Modules.Layout.View;
using SkillProfi.WfpClient.Modules.Main;
using SkillProfi.WfpClient.Modules.Projects;
using SkillProfi.WfpClient.Modules.Services;
using SkillProfi.WfpClient.Modules.Users;
using SkillProfi.WfpClient.Services.Auth;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
	private readonly ServiceProvider _serviceProvider;
	
	public App()
	{
		IServiceCollection services = new ServiceCollection();
		
		services.AddAuthModule();
		services.AddLayoutModule();
		services.AddMainModule();
		services.AddClientRequestModule();
		services.AddServicesModule();
		services.AddProjectsModule();
		services.AddBlogModule();
		services.AddContactsModule();
		services.AddUsersModule();

		services.AddScoped<IClient, AppClient>();
		services.AddSingleton<IAuthService, AuthService>();
		services.AddSingleton<INavigationService, NavigationService>();
		services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));
		
		_serviceProvider = services.BuildServiceProvider();
	}
	
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		
		RootWindow rootWindow = _serviceProvider.GetRequiredService<RootWindow>();
		rootWindow.Show();
		
		ShutdownMode = ShutdownMode.OnMainWindowClose;
	}
}
