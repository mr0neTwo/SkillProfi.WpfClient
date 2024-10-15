using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Contacts.Models;
using SkillProfi.WfpClient.Modules.Contacts.UserControls;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Contacts.View;

public sealed class ContactsViewModel(ContactsApi contactsApi, INavigationService navigationService, CompanyEditorViewModel 
										  companyEditorViewModel, SocialMediaEditorViewModel socialMediaEditorViewModel) : ViewModel
{
	public ObservableCollection<SocialMediaViewModel> SocialMediaViewModels { get; set; } = new();
	
	public Company Company
	{
		get => _company;
		set
		{
			_company = value;
			OnPropertyChanged();
		}
	}

	public DelegateCommand EditCompanyCommand => new(EditCompany);
	public DelegateCommand EditSocialMediaCommand => new(EditSocialMedia);

	private Company _company = new();
	private List<SocialMedia> _socialMedias = new();
	
	protected override void OnBeforeShown()
	{
		_ = UpdateCompany();
		_ = UpdateSocialMedia();
	}

	private async Task UpdateCompany()
	{
		Company = await contactsApi.GetCompanyData() ?? new Company();
	}

	private async Task UpdateSocialMedia()
	{
		SocialMediaViewModels.Clear();
		List<SocialMedia>? socialMediaList = await contactsApi.GetSocialMediaList();

		if (socialMediaList is null)
		{
			return;
		}

		_socialMedias = socialMediaList;

		foreach (SocialMedia socialMedia in socialMediaList)
		{
			SocialMediaViewModel socialMediaViewModel = new();
			socialMediaViewModel.UpdateData(socialMedia.IconName, socialMedia.Link);
			SocialMediaViewModels.Add(socialMediaViewModel);
		}
	}

	private void EditCompany(object obj)
	{
		companyEditorViewModel.EditMode = true;
		companyEditorViewModel.Company = Company;
		navigationService.NavigateTo<CompanyEditorViewModel>();
	}
	
	private void EditSocialMedia(object obj)
	{
		socialMediaEditorViewModel.EditMode = true;
		socialMediaEditorViewModel.SocialMedias = _socialMedias;
		navigationService.NavigateTo<SocialMediaEditorViewModel>();
	}
}
