﻿using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Main.View;

public sealed class MainViewModel(SiteItemApi siteItemApi) : ViewModel
{
	public Dictionary<string, string> SiteItems 
	{ 
		get => _siteItems; 
		set
		{
			_siteItems = value;
			OnPropertyChanged();
		} 
	}

	public DelegateCommand SaveCommand => new(Save, CanSave);

	private Dictionary<string, string> _siteItems = new();
	private Dictionary<string, string> _initialSiteItems = new();
	private bool _isLoading;

	protected override void OnBeforeShown()
	{
		UpdateSiteItems();
	}

	private async void UpdateSiteItems()
	{
		Dictionary<string, string> siteItems = await siteItemApi.GetAllAsync() ?? new Dictionary<string, string>();
		SiteItems = siteItems;
		_initialSiteItems = siteItems.ToDictionary(k => k.Key, v => v.Value);
		OnPropertyChanged(nameof(SiteItems));
	}

	private async void Save(object obj)
	{
		Dictionary<string, string> siteItemsToUpdate = new();

		foreach (string siteItemsKey in SiteItems.Keys)
		{
			if (_initialSiteItems[siteItemsKey] != SiteItems[siteItemsKey])
			{
				siteItemsToUpdate.Add(siteItemsKey, SiteItems[siteItemsKey]);
			}
		}

		if (siteItemsToUpdate.Count == 0)
		{
			return;
		}
		
		_isLoading = true;
		await siteItemApi.UpdateAllAsync(siteItemsToUpdate);
		_isLoading = false;
		UpdateSiteItems();
	}

	private bool CanSave(object obj)
	{
		return !_isLoading;
	}
}
