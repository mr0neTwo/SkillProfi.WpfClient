namespace SkillProfi.WfpClient.Modules.Users;

public struct PasswordValidationResult
{
	public bool IsValid { get; set; }
	public string ErrorMessage { get; set; }
}
