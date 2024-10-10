using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Projects.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Projects.View;

public sealed class ProjectsViewModel(ProjectsApi projectsApi, INavigationService navigationService, ProjectEditorViewModel projectEditorViewModel) : PaginatedViewModel
{
	public ObservableCollection<Project> Projects { get; set; } = new();
	
	public DelegateCommand CreateCommand => new(Create);
	public DelegateCommand EditCommand => new(Edit);
	public DelegateCommand DeleteCommand => new(Delete);
	
	private const int PageSize = 6;

	protected override void OnBeforeShown()
	{
		_ = UpdateProjectsAsync();
	}

	protected override void UpdateData()
	{
		_ = UpdateProjectsAsync();
	}

	private async Task UpdateProjectsAsync()
	{
		GetProjectListRequest request = new()
		{
			PageSize = PageSize, 
			PageNumber = PageNumber
		};

		GetProjectListResponse? response = await projectsApi.GetList(request);

		if (response == null)
		{
			return;
		}
		
		TotalPages = response.TotalPages;
		PageNumber = response.PageNumber;
		Projects = new ObservableCollection<Project>(response.Projects);
		OnPropertyChanged(nameof(Projects));
	}

	private void Create(object obj)
	{
		projectEditorViewModel.EditMode = false;
		navigationService.NavigateTo<ProjectEditorViewModel>();
	}
	
	private void Edit(object obj)
	{
		if (obj is not Project project)
		{
			return;
		}

		projectEditorViewModel.EditMode = true;
		projectEditorViewModel.Project = project;
		navigationService.NavigateTo<ProjectEditorViewModel>();
	}
	
	private void Delete(object obj)
	{
		if (obj is not Project project)
		{
			return;
		}

		_ = DeleteAsync(project.Id);
	}

	private async Task DeleteAsync(int projectId)
	{
		await projectsApi.Delete(projectId);
		await UpdateProjectsAsync();
	}
}
