using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Blog.View;
using SkillProfi.WfpClient.Modules.ClientRequests.View;
using SkillProfi.WfpClient.Modules.Contacts.View;
using SkillProfi.WfpClient.Modules.Main.View;
using SkillProfi.WfpClient.Modules.Projects.View;
using SkillProfi.WfpClient.Modules.Services.View;
using SkillProfi.WfpClient.Modules.Users.View;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Layout.View;

public sealed class LayoutViewModel : ViewModel
{
	public INavigationService NavigationService 
	{ 
		get => _navigationService;
		set 
		{
			_navigationService = value;
			OnPropertyChanged();
		}
	}
	
	public DelegateCommand SelectRequestsCommand => new(SelectRequests);
	public DelegateCommand SelectMainCommand => new(SelectMain);
	public DelegateCommand SelectServicesCommand => new(SelectServices);
	public DelegateCommand SelectProjectsCommand => new(SelectProjects);
	public DelegateCommand SelectBlogCommand => new(SelectBlog);
	public DelegateCommand SelectContactsCommand => new(SelectContacts);
	public DelegateCommand SelectUsersCommand => new(SelectUsers);
	
	private INavigationService _navigationService;

	public LayoutViewModel(INavigationService navigationService)
	{
		_navigationService = navigationService;
		_navigationService.NavigateTo<ClientRequestViewModel>();
	}


	private void SelectRequests(object obj)
	{
		NavigationService.NavigateTo<ClientRequestViewModel>();
	}
	
	private void SelectMain(object obj)
	{
		NavigationService.NavigateTo<MainViewModel>();
	}
	
	private void SelectServices(object obj)
	{
		NavigationService.NavigateTo<ServicesViewModel>();
	}
	
	private void SelectProjects(object obj)
	{
		NavigationService.NavigateTo<ProjectsViewModel>();
	}
	
	private void SelectBlog(object obj)
	{
		NavigationService.NavigateTo<BlogViewModel>();
	}
	
	private void SelectContacts(object obj)
	{
		NavigationService.NavigateTo<ContactsViewModel>();
	}
	
	private void SelectUsers(object obj)
	{
		NavigationService.NavigateTo<UsersViewModel>();
	}
}
