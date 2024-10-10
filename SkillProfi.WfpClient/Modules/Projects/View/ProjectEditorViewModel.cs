using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Projects.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Projects.View;

public sealed class ProjectEditorViewModel(ProjectsApi projectsApi, INavigationService navigationService) : EditorViewModel
{
	public Project Project
	{
		get => _project;
		set
		{
			_project = value;
			TitleInput.Value = value.Title;
			DescriptionInput.Value = value.Description;
			ImageLoader.DefaultImageUrl = value.FullImageUrl;
		}
	}
	
	public LoadImageViewModel ImageLoader { get; set; } = new();

	public InputViewModel TitleInput { get; set; } = new InputViewModel
	{
		Label = "Заголовок", 
		Required = true, 
		Limit = FieldLimits.ProjectTitleMaxLength, 
	};

	public InputViewModel DescriptionInput { get; set; } = new InputViewModel
	{
		Label = "Описание", 
		Required = true, 
		Limit = FieldLimits.ProjectDescriptionMaxLength, 
		Width = 600,
		ShowLimit = true
	};
	
	private Project _project = new();
	

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
		ImageLoader.Clear();
	}

	protected override void Create(object obj)
	{
		if (Validate())
		{
			_ = CreateAsync();
		}
	}

	private async Task CreateAsync()
	{
		CreateProjectDto createProjectDto = new()
		{
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value, 
			ImageBase64 = ImageLoader.ImageBase64
		};

		IsLoading = true;
		await projectsApi.Create(createProjectDto);
		IsLoading = false;
		navigationService.NavigateTo<ProjectsViewModel>();
	}

	protected override void Save(object obj)
	{
		if (Validate())
		{
			_ = SaveAsync();
		}
	}

	private async Task SaveAsync()
	{
		UpdateProjectDto updateProjectDto = new()
		{
			Id = Project.Id, 
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value, 
			ImageBase64 = ImageLoader.ImageBase64
		};
		
		IsLoading = true;
		await projectsApi.Update(updateProjectDto);
		IsLoading = false;
		navigationService.NavigateTo<ProjectsViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<ProjectsViewModel>();
	}

	private bool Validate()
	{
		return DescriptionInput.Validate() && TitleInput.Validate();
	}
}
