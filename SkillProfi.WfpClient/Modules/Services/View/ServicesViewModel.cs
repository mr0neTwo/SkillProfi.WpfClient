using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Services.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Services.View;

public sealed class ServicesViewModel(ServicesApi servicesApi, INavigationService navigationService, ServiceEditorViewModel serviceEditorViewModel) : ViewModel
{
	public ObservableCollection<Service> Services { get; set; } = new();

	public int PageNumber
	{
		get => _pageNumber;
		set
		{
			_pageNumber = value;
			OnPropertyChanged();
			UpdateServices();
		}
	}
	
	public int TotalPages
	{
		get => _totalPages;
		set
		{
			_totalPages = value;
			OnPropertyChanged();
		}
	}

	public DelegateCommand CreateServiceCommand => new(CreateService);
	public DelegateCommand EditServiceCommand => new(EditService);
	public DelegateCommand DeleteServiceCommand => new(DeleteService);
	public DelegateCommand NextPageCommand => new(NextPage, CanNextPage);
	public DelegateCommand PreviousPageCommand => new(PreviousPage, CanPreviousPage);

	private int _pageNumber = 1;
	private int _totalPages = 1;
	private readonly int _pageSize = 14;

	protected override void OnBeforeShown()
	{
		UpdateServices();
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
			UpdateServices();
		}
	}

	private void NextPage(object obj)
	{
		PageNumber++;
	}

	private bool CanNextPage(object obj)
	{
		return PageNumber < TotalPages;
	}
	
	private void PreviousPage(object obj)
	{
		PageNumber--;
	}

	private bool CanPreviousPage(object obj)
	{
		return PageNumber > 1;
	}

	private async void UpdateServices()
	{
		GetServiceListRequest request = new()
		{
			PageNumber = _pageNumber,
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
