using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Contacts.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Contacts.View;

public sealed class CompanyEditorViewModel(ContactsApi contactsApi, INavigationService navigationService) : EditorViewModel
{
	public Company Company
	{
		get => _company;
		set
		{
			_company = value;
			
			NameInput.Value = value.Name;
			EmailInput.Value = value.Email;
			PhoneNumberInput.Value = value.PhoneNumber;
			AddressInput.Value = value.Address;
			DirectorNameInput.Value = value.DirectorName;
			MapLinkInput.Value = value.MapLink;
		}
	}

	public InputViewModel NameInput { get; set; } = new()
	{
		Label = "Компания",
		Required = true,
		Limit = FieldLimits.CompanyNameMaxLength,
	};
	
	public InputViewModel EmailInput { get; set; } = new()
	{
		Label = "Email",
		Required = true,
		Limit = FieldLimits.CompanyEmailMaxLength,
	};
	
	public InputViewModel PhoneNumberInput { get; set; } = new()
	{
		Label = "Телефон",
		Required = true,
		Limit = FieldLimits.CompanyPhoneMaxLength,
	};
	
	public InputViewModel AddressInput { get; set; } = new()
	{
		Label = "Адрес",
		Required = true,
		Limit = FieldLimits.CompanyAddressMaxLength,
	};
	
	public InputViewModel DirectorNameInput { get; set; } = new()
	{
		Label = "Директор",
		Required = true,
		Limit = FieldLimits.CompanyDirectorNameMaxLength,
	};
	
	public InputViewModel MapLinkInput { get; set; } = new()
	{
		Label = "Ссылка Google map",
		Required = true,
		Limit = FieldLimits.CompanyMapLinkMaxLength,
	};
	
	
	private Company _company = new();

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
		UpdateCompanyDto updateCompanyDto = new()
		{
			Name = NameInput.Value,
			Email = EmailInput.Value,
			PhoneNumber = PhoneNumberInput.Value,
			Address = AddressInput.Value,
			DirectorName = DirectorNameInput.Value,
			MapLink = MapLinkInput.Value
		};
		
		await contactsApi.UpdateCompanyData(updateCompanyDto);

		navigationService.NavigateTo<ContactsViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<ContactsViewModel>();
	}
	
	private bool Validate()
	{
		return NameInput.Validate() 
			   && EmailInput.Validate() 
			   && PhoneNumberInput.Validate() 
			   && AddressInput.Validate() 
			   && DirectorNameInput.Validate() 
			   && MapLinkInput.Validate();
	}
}
