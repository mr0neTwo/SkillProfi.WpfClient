using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Services.View;

public sealed class ServiceEditorViewModel(INavigationService navigationService, ServicesApi servicesApi) : EditorViewModel
{
	public Service Service
	{
		get => _service;
		set
		{
			_service = value;
			TitleInput.Value = value.Title;
			DescriptionInput.Value = value.Description;
		}
	}

	public InputViewModel TitleInput { get; set; } = new InputViewModel
	{
		Label = "Заголовок", 
		Required = true, 
		Limit = FieldLimits.ServiceTitleMaxLength, 
	};

	public InputViewModel DescriptionInput { get; set; } = new InputViewModel
	{
		Label = "Описание", 
		Required = true, 
		Limit = FieldLimits.ProjectDescriptionMaxLength, 
		Width = 600,
		ShowLimit = true
	};
	
	private Service _service = new();

	protected override void OnBeforeShown()
	{
		if (!EditMode)
		{
			ClearForms();
		}
	}
	
	private void ClearForms()
	{
		TitleInput.Clear();
		DescriptionInput.Clear();
		Service = new Service();
	}

	protected override void Create(object obj)
	{
		if (ValidateField())
		{
			_ = CreateAsync();
		}
	}

	private async Task CreateAsync()
	{
		CreateServiceDto createServiceDto = new()
		{
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value,
		};
		
		IsLoading = true;
		await servicesApi.Create(createServiceDto);
		IsLoading = false;
		navigationService.NavigateTo<ServicesViewModel>();
	}
	
	protected override void Save(object obj)
	{
		if (ValidateField())
		{
			_ = SaveAsync();
		}
	}

	private async Task SaveAsync()
	{
		UpdateServiceDto updateServiceDto = new()
		{
			Id = Service.Id, 
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value,
		};
		
		IsLoading = true;
		await servicesApi.Update(updateServiceDto);
		IsLoading = false;
		navigationService.NavigateTo<ServicesViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<ServicesViewModel>();
	}

	private bool ValidateField()
	{
		return DescriptionInput.Validate() && TitleInput.Validate();
	}
}
