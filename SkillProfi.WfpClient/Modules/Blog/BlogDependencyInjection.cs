using Microsoft.Extensions.DependencyInjection;
using SkillProfi.WfpClient.Modules.Blog.View;

namespace SkillProfi.WfpClient.Modules.Blog;

public static class BlogDependencyInjection
{
	public static IServiceCollection AddBlogModule(this IServiceCollection services)
	{
		services.AddSingleton<BlogApi>();
		services.AddSingleton<BlogViewModel>();
		services.AddSingleton<BlogEditorViewModel>();
		
		return services;
	}
}
