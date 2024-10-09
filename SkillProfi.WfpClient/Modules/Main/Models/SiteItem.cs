using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Main.Models;

public sealed class SiteItem : IApiResponse, IApiRequest
{
	public string Key { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
}
