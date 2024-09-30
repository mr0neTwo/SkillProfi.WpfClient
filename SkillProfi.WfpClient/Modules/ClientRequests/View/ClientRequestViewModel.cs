using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.ClientRequests.Models;

namespace SkillProfi.WfpClient.Modules.ClientRequests.View;

public sealed class ClientRequestViewModel : ViewModel
{
	public ObservableCollection<ClientRequest> ClientRequests { get; set; } = new();
	
	private AppClientRequestApi _api;

	public ClientRequestViewModel(AppClientRequestApi api)
	{
		_api = api;
	}

	protected override void OnBeforeShown()
	{
		UpdateClientRequests();
	}

	private async void UpdateClientRequests()
	{
		GetClientApiRequestListApiRequest request = new()
		{
			StartTimestamp = new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeSeconds(),
			EndTimeStamp = new DateTimeOffset(new DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc)).ToUnixTimeSeconds(),
			PageNumber = 1,
			PageSize = 20
		};

		GetClientRequestListResponse? response = await _api.GetClientRequests(request);

		if (response is null)
		{
			return;
		}

		ClientRequests.Clear();

		foreach (ClientRequest clientRequest in response.ClientRequests)
		{
			ClientRequests.Add(clientRequest);
		}
	}
}
