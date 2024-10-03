using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Auth.View;
using SkillProfi.WfpClient.Services.Auth;

namespace SkillProfi.WfpClient.Modules.Layout.View;

public sealed class RootViewModel : ViewModel
{
	public ViewModel CurrentView
	{
		get => _currentView;
		set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}
  
	private ViewModel _currentView;
	
	private readonly IAuthService _authService;
	private readonly LayoutViewModel _layoutViewModel;
	private readonly AuthViewModel _authViewModel;

	public RootViewModel(IAuthService authService, LayoutViewModel layoutViewModel, AuthViewModel authViewModel)
	{
		_authService = authService;
		_layoutViewModel = layoutViewModel;
		_authViewModel = authViewModel;
		
		_currentView = _authService.LoggedIn ? _layoutViewModel : _authViewModel;
		_authService.LoginStatusChanged += OnLoginStatusChanged;
	}

	private void OnLoginStatusChanged(bool loggedIn)
	{
		if (loggedIn)
		{
			_layoutViewModel.BeforeShown();
			CurrentView = _layoutViewModel;
		}
		else
		{
			_authViewModel.BeforeShown();
			CurrentView = _authViewModel;
		}
	}
}
