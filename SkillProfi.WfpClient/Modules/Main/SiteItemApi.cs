using SkillProfi.WfpClient.Common.Client;

namespace SkillProfi.WfpClient.Modules.Main;

public sealed class SiteItemApi(IClient client)
{
	public async Task<Dictionary<string, string>?> GetAllAsync()
	{
		Dictionary<string, string>? siteItem = await client.GetAsync<Dictionary<string, string>>("api/SiteItem/GetAll");
		
		return siteItem;
	}

	public async Task UpdateAllAsync(Dictionary<string, string> siteItemDictionary)
	{
		await client.PutAsync(siteItemDictionary, "api/SiteItem/UpdateAll");
	}
}
