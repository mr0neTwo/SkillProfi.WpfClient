using System.Net.Http;

namespace SkillProfi.WfpClient.Common;

public class BaseApi
{
	protected HttpClient Client { get; set; }
	
	public BaseApi()
	{
		// HttpClientHandler handler = new()
		// {
		// 	UseCookies = true,                              
		// 	Credentials = CredentialCache.DefaultCredentials
		// };
		
		Client = new HttpClient();
		Client.BaseAddress = new Uri("http://localhost:5272/");
	}
}