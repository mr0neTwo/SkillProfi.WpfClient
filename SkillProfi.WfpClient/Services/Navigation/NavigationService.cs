using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Services.Navigation;

public sealed class NavigationService(Func<Type, ViewModel> viewModelFactory) : ObservableObject, INavigationService
{
	public ViewModel? CurrentView
	{
		get => _currentView;
		private set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}


	private ViewModel? _currentView;

	public void NavigateTo<TViewModel>() where TViewModel : ViewModel
	{
		ViewModel viewModel = viewModelFactory.Invoke(typeof(TViewModel));
		viewModel.BeforeShown();
		CurrentView = viewModel;
	}
}
