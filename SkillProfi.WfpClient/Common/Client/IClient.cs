namespace SkillProfi.WfpClient.Common.Client;

public interface IClient
{
	public Task<TResponseBody?> GetAsync<TResponseBody, TRequest>(TRequest apiRequest, string url);

	public Task<TResponseBody?> GetAsync<TResponseBody>(string url);

	public Task<TResponseBody?> PostAsync<TResponseBody, TRequest>(TRequest apiRequest, string url);

	public Task PostVoidAsync<TRequest>(TRequest apiRequest, string url);

	public Task PutAsync<TRequest>(TRequest dto, string url);

	public Task DeleteAsync(int id, string url);
}
