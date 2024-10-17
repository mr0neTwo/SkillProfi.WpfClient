using SkillProfi.WfpClient.Common.Client;
using SkillProfi.WfpClient.Modules.Users.Models;

namespace SkillProfi.WfpClient.Modules.Users;

public class UsersApi(IClient client)
{
	public async Task<List<User>?> GetList()
	{
		string url = "api/User/GetList";
		List<User>? users = await client.GetAsync<List<User>>(url);

		return users;
	}
	
	public async Task Create(CreateUserDto createUserDto)
	{
		string url = "api/User/Create";
		await client.PostVoidAsync(createUserDto, url);
	}

	public async Task Update(UpdateUserDto updateUserDto)
	{
		string url = "api/User/Update";
		await client.PutAsync(updateUserDto, url);
	}

	public async Task Delete(int userId)
	{
		string url = "api/User/Delete";
		await client.DeleteAsync(userId, url);
	}
}
