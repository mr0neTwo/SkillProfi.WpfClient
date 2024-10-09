using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Services.View;

public sealed class ServiceEditorViewModel(INavigationService navigationService, ServicesApi servicesApi) : ViewModel
{
	public Service Service
	{
		get => _service;
		set
		{
			_service = value;
			OnPropertyChanged();
		}
	}

	public bool IsTittleCorrect
	{
		get => _isTitleCorrect;
		set
		{
			_isTitleCorrect = value;
			OnPropertyChanged();
		}
	}

	public bool IsDescriptionCorrect
	{
		get => _isDescriptionCorrect;
		set
		{
			_isDescriptionCorrect = value;
			OnPropertyChanged();
		}
	}

	public bool ShowCreateButton
	{
		get => _showCreateButton;
		set
		{
			_showCreateButton = value;
			OnPropertyChanged();
		}
	}
	
	public bool ShowSaveButton
	{
		get => _showSaveButton;
		set
		{
			_showSaveButton = value;
			OnPropertyChanged();
		}
	}

	public bool EditMode
	{
		get => _editMode;
		set
		{
			_editMode = value;
			ShowCreateButton = !value;
			ShowSaveButton = value;
		}
	}

	public DelegateCommand CreateCommand => new(Create, CanCreate);
	public DelegateCommand SaveCommand => new(Save, CanSave);
	public DelegateCommand GoBackCommand => new(GoBack, CanGoBack);
	
	private Service _service = new();
	private bool _isLoading;
	private bool _editMode;
	private bool _showCreateButton;
	private bool _showSaveButton;
	private bool _isTitleCorrect = true;
	private bool _isDescriptionCorrect = true;

	protected override void OnBeforeShown()
	{
		if (!EditMode)
		{
			Service = new Service();
		}
	}

	private void Create(object obj)
	{
		_ = CreateAsync();
	}

	private async Task CreateAsync()
	{
		if (!ValidateField())
		{
			return;
		}
		
		_isLoading = true;
		await servicesApi.Create(Service);
		_isLoading = false;
		navigationService.NavigateTo<ServicesViewModel>();
	}

	private bool CanCreate(object obj)
	{
		return !_isLoading;
	}
	
	private void Save(object obj)
	{
		_ = SaveAsync();
	}

	private async Task SaveAsync()
	{
		if (!ValidateField())
		{
			return;
		}
		
		_isLoading = true;
		await servicesApi.Update(Service);
		_isLoading = false;
		navigationService.NavigateTo<ServicesViewModel>();
	}

	private bool CanSave(object obj)
	{
		return !_isLoading;
	}

	private void GoBack(object obj)
	{
		navigationService.NavigateTo<ServicesViewModel>();
	}
	
	private bool CanGoBack(object obj)
	{
		return !_isLoading;
	}

	private bool ValidateField()
	{
		IsTittleCorrect = !string.IsNullOrEmpty(Service.Title);
		IsDescriptionCorrect = !string.IsNullOrEmpty(Service.Description);
		
		return IsTittleCorrect && IsDescriptionCorrect;
	}
}
