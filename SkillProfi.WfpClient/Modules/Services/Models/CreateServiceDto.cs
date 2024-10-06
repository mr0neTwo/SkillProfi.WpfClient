﻿using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Modules.Services.Models;

public sealed class CreateServiceDto : ApiRequest
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}