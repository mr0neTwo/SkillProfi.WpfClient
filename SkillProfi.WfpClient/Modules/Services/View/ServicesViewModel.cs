using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Services.View;

public sealed class ServicesViewModel(ServicesApi servicesApi, INavigationService navigationService, ServiceEditorViewModel serviceEditorViewModel) : PaginatedViewModel
{
	public ObservableCollection<Service> Services { get; set; } = new();

	public DelegateCommand CreateServiceCommand => new(CreateService);
	public DelegateCommand EditServiceCommand => new(EditService);
	public DelegateCommand DeleteServiceCommand => new(DeleteService);
	
	private readonly int _pageSize = 14;

	protected override void OnBeforeShown()
	{
		UpdateData();
	}

	private void CreateService(object obj)
	{
		serviceEditorViewModel.EditMode = false;
		navigationService.NavigateTo<ServiceEditorViewModel>();
	}
	
	private void EditService(object obj)
	{
		if (obj is Service service)
		{
			serviceEditorViewModel.EditMode = true;
			serviceEditorViewModel.Service = service;
			navigationService.NavigateTo<ServiceEditorViewModel>();
		}
	}
	
	private async void DeleteService(object obj)
	{
		if (obj is Service service)
		{
			await servicesApi.Delete(service.Id);
			UpdateData();
		}
	}

	protected override async void UpdateData()
	{
		GetServiceListRequest request = new()
		{
			PageNumber = PageNumber,
			PageSize = _pageSize
		};
		
		GetServiceListResponse? response = await servicesApi.GetList(request);

		if (response is null)
		{
			return;
		}

		TotalPages = response.TotalPages;
		Services = new ObservableCollection<Service>(response.Services);
		OnPropertyChanged(nameof(Services));
	}
}
