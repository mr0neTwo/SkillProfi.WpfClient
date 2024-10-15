using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Contacts.UserControls;

namespace SkillProfi.WfpClient.Modules.Contacts.View;

public sealed class SocialMediaForm
{
	public IconSelectorViewModel IconSelector { get; set; } = new();
	public InputViewModel LinkInput { get; set; } = new();
}
