using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Contacts.UserControls;

public sealed class IconSelectorViewModel : ViewModel
{
	public string SelectedIconData
	{
		get => _selectedIconData;
		set
		{
			_selectedIconData = value;
			OnPropertyChanged();
		}
	}

	public bool ShowIcons
	{
		get => _showIcons;
		set
		{
			_showIcons = value;
			OnPropertyChanged();
		}
	}

	public string SelectedIconKey
	{
		get => _selectedIconKey;
		set
		{
			if (IconData.TryGetValue(value, out string iconData))
			{
				SelectedIconData = iconData;
				_selectedIconKey = value;
			}
		}
	}

	public Dictionary<string, string> IconData { get; set; } = Icon.AllValues;
	
	public DelegateCommand ToggleIconsCommand => new(ToggleIcon);
	public DelegateCommand SelectIconCommand => new(SelectIcon);

	private const string DefaultIconKey = "YOUTUBE";
	private string _selectedIconData = Icon.GetIconData(DefaultIconKey);
	private string _selectedIconKey = DefaultIconKey;
	private bool _showIcons;

	private void ToggleIcon(object obj)
	{
		ShowIcons = true;
	}

	private void SelectIcon(object obj)
	{
		if (obj is string iconKey)
		{
			SelectedIconData = IconData[iconKey];
			SelectedIconKey = iconKey;
			ShowIcons = false;
		}
	}
}
