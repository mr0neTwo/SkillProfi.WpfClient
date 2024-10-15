using SkillProfi.WfpClient.Modules.Contacts.Models;
using SkillProfi.WfpClient.Services.Client;

namespace SkillProfi.WfpClient.Modules.Contacts;

public sealed class ContactsApi(IClient client)
{
	public async Task<Company?> GetCompanyData()
	{
		string url = "api/company/get";
		Company? companyData = await client.GetAsync<Company>(url);
		
		return companyData;
	}

	public async Task UpdateCompanyData(UpdateCompanyDto updateCompanyDto)
	{
		string url = "api/company/update";
		await client.PutAsync(updateCompanyDto, url);
	}

	public async Task<List<SocialMedia>?> GetSocialMediaList()
	{
		string url = "api/socialMedia/getList";
		List<SocialMedia>? socialMediaList = await client.GetAsync<List<SocialMedia>>(url);
		
		return socialMediaList;
	}

	public async Task UpdateSocialMediaList(List<SocialMedia> socialMediaList)
	{
		string url = "api/socialMedia/UpdateAll";
		await client.PutAsync(socialMediaList, url);
	}
}
