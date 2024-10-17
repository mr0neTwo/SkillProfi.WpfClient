using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Auth.Models;
using SkillProfi.WfpClient.Services.Auth;

namespace SkillProfi.WfpClient.Modules.Auth.View;

public sealed class AuthViewModel(IAuthService authService) : ViewModel
{
	public InputViewModel EmailInput { get; set; } = new()
	{
		Label = "Email",
		Required = true,
		Value = "admin",
		Limit = FieldLimits.UserEmailMaxLength
	};
	
	public InputViewModel PasswordInput { get; set; } = new()
	{
		Label = "Пароль",
		Required = true,
		Value = "123456",
		Limit = FieldLimits.UserPasswordMaxLength
	};
	
	public string Error
	{
		get => _error;
		set
		{
			_error = value;
			OnPropertyChanged();
		}
	}
	
	public DelegateCommand LoginCommand => new(Login);
	
	private string _error = string.Empty;

	private void Login(object obj)
	{
		_ = LoginAsync();
	}

	private async Task LoginAsync()
	{
		UserLoginDto userLoginDto = new()
		{
			Email = EmailInput.Value,
			Password = PasswordInput.Value
		};
		
		AuthResponse? result = await authService.LoginAsync(userLoginDto);

		if (result != null && result.Success)
		{
			ResetForms();

			return;
		}
		
		Error = result?.ErrorMessage ?? "Неверный логин или пароль";
	}

	private void ResetForms()
	{
		EmailInput.Clear();
		PasswordInput.Clear();
		Error = string.Empty;
	}
}
