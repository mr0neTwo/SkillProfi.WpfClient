using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.ClientRequests.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.ClientRequests.View;

public sealed class ClientRequestViewModel : PaginatedViewModel
{
	public ObservableCollection<ClientRequest> ClientRequests { get; set; }
	public ObservableCollection<string> DateRanges { get; set; }

	public string SelectedDateRange
	{
		get => _selectedDateRange;
		set
		{
			_selectedDateRange = value;
			OnPropertyChanged();
			UpdateDateRange();
			UpdateData();
		}
	}

	public DelegateCommand ClientRequestDoubleClickCommand => new(OnClientRequestDoubleClick);

	private const int PageSize = 23;
	
	private string _selectedDateRange = string.Empty;
	private long _startTime;
	private long _endTime;
	private readonly ClientRequestApi _api;
	private readonly INavigationService _navigationService;
	private readonly ClientRequestEditorViewModel _editorViewModel;

	public ClientRequestViewModel(ClientRequestApi api, INavigationService navigationService, ClientRequestEditorViewModel editorViewModel)
	{
		_editorViewModel = editorViewModel;
		_navigationService = navigationService;
		_api = api;
		
		_startTime = 0;
		_endTime = TimestampUtils.GetEndOfDayTimestamp();

		DateRanges = ["Все время", "Сегодня", "Текущая неделя", "Текущий месяц"];
		SelectedDateRange = DateRanges.First();
		ClientRequests = [];
	}

	protected override void OnBeforeShown()
	{
		_ = UpdateClientRequests();
	}

	protected override void UpdateData()
	{
		_ = UpdateClientRequests();
	}

	private void OnClientRequestDoubleClick(object selectedItem)
	{
		if (selectedItem is not ClientRequest clientRequest)
		{
			return;
		}

		_editorViewModel.ClientRequest = clientRequest;
		_navigationService.NavigateTo<ClientRequestEditorViewModel>();
	}

	private void UpdateDateRange()
	{
		switch (_selectedDateRange)
		{
			case "Все время":
			{
				_startTime = TimestampUtils.MinValue;
				_endTime = TimestampUtils.MaxValue;

				break;
			}

			case "Сегодня":
			{
				_startTime = TimestampUtils.GetStartOfDayTimestamp();
				_endTime = TimestampUtils.GetEndOfDayTimestamp();

				break;
			}

			case "Текущая неделя":
			{
				_startTime = TimestampUtils.GetStartOfWeekTimestamp();
				_endTime = TimestampUtils.GetEndOfWeekTimestamp();

				break;
			}

			case "Текущий месяц":
			{
				_startTime = TimestampUtils.GetStartOfMonthTimestamp();
				_endTime = TimestampUtils.GetEndOfMonthTimestamp();

				break;
			}
		}
	}

	private async Task UpdateClientRequests()
	{
		GetClientApiRequestListApiRequest request = new()
		{
			StartTimestamp = _startTime, EndTimeStamp = _endTime, PageNumber = PageNumber, PageSize = PageSize
		};

		GetClientRequestListResponse? response = await _api.GetClientRequests(request);

		if (response is null)
		{
			return;
		}

		TotalPages = response.TotalPages;
		PageNumber = response.PageNumber;

		ClientRequests.Clear();

		foreach (ClientRequest clientRequest in response.ClientRequests)
		{
			ClientRequests.Add(clientRequest);
		}
	}
}
