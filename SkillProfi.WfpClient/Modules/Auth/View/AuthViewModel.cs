using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Auth.Models;
using SkillProfi.WfpClient.Services.Auth;

namespace SkillProfi.WfpClient.Modules.Auth.View;

public sealed class AuthViewModel(IAuthService authService) : ViewModel
{
	public string Email
	{
		get => _email;
		set
		{
			_email = value;
			OnPropertyChanged();
		}
	}
	
	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}
	
	public string Error
	{
		get => _error;
		set
		{
			_error = value;
			OnPropertyChanged();
		}
	}

	public InputViewModel LoginInput { get; set; } = new() { Label = "Введите текст", Required = true, Limit = 50 };

	public DelegateCommand LoginCommand => new(Login);

	private string _email = "admin";
	private string _password = "123456";
	private string _error = string.Empty;

	private void Login(object obj)
	{
		_ = LoginAsync();
	}

	private async Task LoginAsync()
	{
		UserLoginDto userLoginDto = new()
		{
			Email = Email,
			Password = Password
		};
		
		AuthResponse? result = await authService.LoginAsync(userLoginDto);

		if (result != null && result.Success)
		{
			ResetForms();

			return;
		}
		
		Error = result?.ErrorMessage ?? "Сервер не отвечает";
	}

	private void ResetForms()
	{
		Email = string.Empty;
		Password = string.Empty;
	}
}
