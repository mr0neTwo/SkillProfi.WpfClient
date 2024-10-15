using System.Diagnostics;
using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Contacts.UserControls;

public sealed class SocialMediaViewModel : ViewModel
{
	public string IconData
	{
		get => _iconData;
		set
		{
			_iconData = value;
			OnPropertyChanged();
		}
	}
	
	public string Link { get; set; } = string.Empty;

	public DelegateCommand GoToLinkCommand => new(GoToLink);

	private string _iconData = string.Empty;

	public void UpdateData(string iconKey, string link)
	{
		IconData = Icon.GetIconData(iconKey);
		Link = link;
	}

	private void GoToLink(object obj)
	{
		if (!string.IsNullOrWhiteSpace(Link))
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				FileName = Link,
				UseShellExecute = true
			};

			try
			{
				Process.Start(processStartInfo);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
