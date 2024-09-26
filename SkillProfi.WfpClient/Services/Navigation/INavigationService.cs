using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Services.Navigation;

public interface INavigationService
{
	public ViewModel? CurrentView { get; }

	public void NavigateTo<T>() where T : ViewModel;
}
