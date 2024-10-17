using SkillProfi.WfpClient.Common.Client;
using SkillProfi.WfpClient.Modules.Blog.Models;

namespace SkillProfi.WfpClient.Modules.Blog;

public sealed class BlogApi(IClient client)
{
	public async Task<GetPostListResponse?> GetList(GetPostListRequest request)
	{
		string url = "api/Post/GetList";
		GetPostListResponse? response = await client.GetAsync<GetPostListResponse, GetPostListRequest>(request, url);
		
		return response;
	}

	public async Task Create(CreatePostDto createPostDto)
	{
		string url = "api/Post/Create";
		await client.PostVoidAsync(createPostDto, url);
	}

	public async Task Update(UpdatePostDto updatePostDto)
	{
		string url = "api/Post/Update";
		await client.PutAsync(updatePostDto, url);
	}

	public async Task Delete(int postId)
	{
		string url = "api/Post/Delete";
		await client.DeleteAsync(postId, url);
	}
}
