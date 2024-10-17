namespace SkillProfi.WfpClient.Modules.Users;

public static class PasswordValidator
{
	public static PasswordValidationResult ValidatePassword(string password)
	{
		bool hasUpperCase = password.Any(char.IsUpper);
		bool hasLowerCase = password.Any(char.IsLower);
		bool hasNumber = password.Any(char.IsDigit);
		bool hasSpecialChar = password.Any(ch => "!@#$%^&*(),.?\":{}|<>".Contains(ch));
		bool isLongEnough = password.Length >= 6;

		if (!isLongEnough)
		{
			return new PasswordValidationResult { IsValid = false, ErrorMessage = "Пароль должен быть не менее 6 символов" };
		}
		
		if (!hasUpperCase)
		{
			return new PasswordValidationResult { IsValid = false, ErrorMessage = "Пароль должен содержать хотя бы одну заглавную букву" };
		}
		
		if (!hasLowerCase)
		{
			return new PasswordValidationResult { IsValid = false, ErrorMessage = "Пароль должен содержать хотя бы одну строчную букву" };
		}
		
		if (!hasNumber)
		{
			return new PasswordValidationResult { IsValid = false, ErrorMessage = "Пароль должен содержать хотя бы одну цифру" };
		}
		
		if (!hasSpecialChar)
		{
			return new PasswordValidationResult { IsValid = false, ErrorMessage = "Пароль должен содержать хотя бы один специальный символ" };
		}

		return new PasswordValidationResult { IsValid = true, ErrorMessage = string.Empty };
	}
}