using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Contacts.Models;
using SkillProfi.WfpClient.Modules.Contacts.UserControls;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Contacts.View;

public sealed class SocialMediaEditorViewModel(ContactsApi contactsApi, INavigationService navigationService) : EditorViewModel
{
	public ObservableCollection<SocialMediaForm> SocialMediaForms { get; set; } = new();

	public List<SocialMedia> SocialMedias
	{
		get => _socialMedias;
		set
		{
			_socialMedias = value;
			UpdateForms(value);
		}
	}

	public DelegateCommand AddCommand => new(Add);
	public DelegateCommand DeleteCommand => new(Delete);

	private List<SocialMedia> _socialMedias = new();

	private void UpdateForms(List<SocialMedia> socialMedias)
	{
		SocialMediaForms.Clear();

		foreach (SocialMedia socialMedia in socialMedias)
		{
			IconSelectorViewModel iconSelectorViewModel = new()
			{
				SelectedIconKey = socialMedia.IconName, 
			};

			InputViewModel linkInput = new()
			{
				Label = "Ссылка",
				Value = socialMedia.Link,
				Required = true,
				Limit = FieldLimits.SocialMediaLinkMaxLength,
			};
			
			SocialMediaForm socialMediaForm = new()
			{
				IconSelector = iconSelectorViewModel,
				LinkInput = linkInput
			};
			
			SocialMediaForms.Add(socialMediaForm);
		}
	}

	private void Add(object obj)
	{
		SocialMediaForm socialMediaForm = new()
		{
			IconSelector = new IconSelectorViewModel(),
			LinkInput = new InputViewModel() { Value = string.Empty, Required = true, Label = "Ссылка" }
		};
		
		SocialMediaForms.Add(socialMediaForm);
	}

	private void Delete(object obj)
	{
		if (obj is SocialMediaForm socialMediaForm)
		{
			SocialMediaForms.Remove(socialMediaForm);
		}
	}

	protected override void Create(object obj)
	{
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
		List<SocialMedia> updatedList = new();

		foreach (SocialMediaForm socialMediaForm in SocialMediaForms)
		{
			SocialMedia socialMedia = new()
			{
				IconName = socialMediaForm.IconSelector.SelectedIconKey, 
				Link = socialMediaForm.LinkInput.Value
			};
				
			updatedList.Add(socialMedia);
		}

		await contactsApi.UpdateSocialMediaList(updatedList);
		navigationService.NavigateTo<ContactsViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<ContactsViewModel>();
	}

	private bool Validate()
	{
		return SocialMediaForms.All(socialMediaForm => socialMediaForm.LinkInput.Validate());
	}
}