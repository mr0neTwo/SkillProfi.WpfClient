using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.ClientRequests.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.ClientRequests.View;

public sealed class ClientRequestEditorViewModel(INavigationService navigationService, ClientRequestApi api) : ViewModel
{
	public ObservableCollection<string> StatusOptions { get; set; } = ["Получен", "В работе", "Выполнен", "Отклонен", "Отменен"];

	public string SelectedStatus
	{
		get => _selectedStatus;
		set
		{
			_selectedStatus = value;
			OnPropertyChanged();
			ChangeStatus(value);
		}
	}

	public ClientRequest ClientRequest
	{
		get => _clientRequests;
		set
		{
			_clientRequests = value;
			OnPropertyChanged();
			ChangeSelectedStatus(value.Status);
		}
	}

	public DelegateCommand SaveCommand => new(Save, CanSave);
	public DelegateCommand GoBackCommand => new(GoBack, CanGoBack);

	private ClientRequest _clientRequests = new();
	private string _selectedStatus = string.Empty;
	private bool _isLoading;

	private async void Save(object obj)
	{
		_isLoading = true;
		await api.UpdateClientRequest(ClientRequest);
		_isLoading = false;
		navigationService.NavigateTo<ClientRequestViewModel>();
	}

	private bool CanSave(object obj)
	{
		return !_isLoading;
	}

	private void GoBack(object obj)
	{
		navigationService.NavigateTo<ClientRequestViewModel>();
	}
	
	private bool CanGoBack(object obj)
	{
		return !_isLoading;
	}
	
	private void ChangeStatus(string status)
	{
		_clientRequests.Status = StatusConverter.ToEnum(status);
	}
	
	private void ChangeSelectedStatus(ClientRequestStatus status)
	{
		SelectedStatus = StatusConverter.ToString(status);
	}
}
