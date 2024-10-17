using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Users.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Users.View;

public sealed class UserEditorViewModel(UsersApi usersApi, INavigationService navigationService) : EditorViewModel
{
	public User User
	{
		get => _user;
		set
		{
			_user = value;
			NameInput.Value = value.Name ?? string.Empty;
			EmailInput.Value = value.Email;
		}
	}
	
	public InputViewModel NameInput { get; set; } = new InputViewModel
	{
		Label = "Имя", 
		Required = true, 
		Limit = FieldLimits.UserNameMaxLength, 
	};
	
	public InputViewModel EmailInput { get; set; } = new InputViewModel
	{
		Label = "Email", 
		Required = true, 
		Limit = FieldLimits.UserEmailMaxLength, 
	};
	
	public InputViewModel PasswordInput { get; set; } = new InputViewModel
	{
		Label = "Пароль", 
		Required = true, 
		Limit = FieldLimits.UserPasswordMaxLength
	};

	public string ErrorMessage
	{
		get => _errorMessage;
		set
		{
			_errorMessage = value;
			OnPropertyChanged();
		}
	}

	private User _user = new();
	private string _errorMessage = string.Empty;

	protected override void OnBeforeShown()
	{
		if (!EditMode)
		{
			ClearForms();
		}

		PreparePasswordInput();
	}

	private void PreparePasswordInput()
	{
		PasswordInput.Clear();
		PasswordInput.Required = !EditMode;
		ErrorMessage = string.Empty;
	}

	private void ClearForms()
	{
		NameInput.Clear();
		EmailInput.Clear();
	}

	protected override void Create(object obj)
	{
		if (Validate())
		{
			_ = CreateAsync();
		}
	}

	private async Task CreateAsync()
	{
		CreateUserDto createUserDto = new()
		{
			Name = NameInput.Value, 
			Email = EmailInput.Value, 
			Password = PasswordInput.Value
		};
		
		await usersApi.Create(createUserDto);
		navigationService.NavigateTo<UsersViewModel>();
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
		UpdateUserDto updateUserDto = new()
		{
			Id = User.Id,
			Name = NameInput.Value, 
			Email = EmailInput.Value, 
			Password = string.IsNullOrEmpty(PasswordInput.Value) ? null : PasswordInput.Value
		};
		
		await usersApi.Update(updateUserDto);
		navigationService.NavigateTo<UsersViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<UsersViewModel>();
	}
	
	private bool Validate()
	{
		return NameInput.Validate() && EmailInput.Validate() && ValidatePassword();
	}

	private bool ValidatePassword()
	{
		if (EditMode && string.IsNullOrEmpty(PasswordInput.Value))
		{
			return true;
		}

		PasswordValidationResult result = PasswordValidator.ValidatePassword(PasswordInput.Value);
		ErrorMessage = result.ErrorMessage;
		
		return result.IsValid;
	}
}
