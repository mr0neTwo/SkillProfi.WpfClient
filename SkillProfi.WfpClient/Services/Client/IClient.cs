using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Services.Client;

public interface IClient
{
	public Task<TResponseBody?> GetAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
		where TResponseBody : IApiResponse
		where TRequest : IApiRequest;

	public Task<TResponseBody?> GetAsync<TResponseBody>(string url);

	public Task<TResponseBody?> PostAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
		where TResponseBody : IApiResponse
		where TRequest : IApiRequest;

	public Task PostVoidAsync<TRequest>(TRequest apiRequest, string url) 
		where TRequest : IApiRequest;

	public Task PutAsync<TRequest>(TRequest dto, string url);

	public Task DeleteAsync(int id, string url);
}
