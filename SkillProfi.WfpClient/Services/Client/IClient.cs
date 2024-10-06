using SkillProfi.WfpClient.Common;

namespace SkillProfi.WfpClient.Services.Client;

public interface IClient
{
	public Task<TResponseBody?> GetAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
		where TResponseBody : ApiResponseBody
		where TRequest : ApiRequest;

	public Task<TResponseBody?> GetAsync<TResponseBody>(string url)
		where TResponseBody : ApiResponseBody;

	public Task<TResponseBody?> PostAsync<TResponseBody, TRequest>(TRequest apiRequest, string url)
		where TResponseBody : ApiResponseBody
		where TRequest : ApiRequest;

	public Task PostVoidAsync<TRequest>(TRequest apiRequest, string url) 
		where TRequest : ApiRequest;

	public Task PutAsync<TRequest>(TRequest dto, string url)
		where TRequest : ApiRequest;

	public Task DeleteAsync(int id, string url);
}
