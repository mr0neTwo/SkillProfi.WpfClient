using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Blog.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Blog.View;

public sealed class BlogViewModel(BlogApi blogApi, INavigationService navigationService, BlogEditorViewModel blogEditorViewModel) : PaginatedViewModel
{
	public ObservableCollection<Post> Posts { get; set; } = new();
	
	public DelegateCommand CreateCommand => new(Create);
	public DelegateCommand EditCommand => new(Edit);
	public DelegateCommand DeleteCommand => new(Delete);
	
	private const int PageSize = 3;

	protected override void OnBeforeShown()
	{
		_ = UpdatePostAsync();
	}

	protected override void UpdateData()
	{
		_ = UpdatePostAsync();
	}

	private async Task UpdatePostAsync()
	{
		GetPostListRequest request = new()
		{
			PageSize = PageSize, 
			PageNumber = PageNumber
		};

		GetPostListResponse? response = await blogApi.GetList(request);

		if (response == null)
		{
			return;
		}
		
		TotalPages = response.TotalPages;
		PageNumber = response.PageNumber;
		Posts = new ObservableCollection<Post>(response.Posts);
		OnPropertyChanged(nameof(Posts));
	}

	private void Create(object obj)
	{
		blogEditorViewModel.EditMode = false;
		navigationService.NavigateTo<BlogEditorViewModel>();
	}
	
	private void Edit(object obj)
	{
		if (obj is not Post post)
		{
			return;
		}

		blogEditorViewModel.EditMode = true;
		blogEditorViewModel.Post = post;
		navigationService.NavigateTo<BlogEditorViewModel>();
	}
	
	private void Delete(object obj)
	{
		if (obj is not Post post)
		{
			return;
		}

		_ = DeleteAsync(post.Id);
	}

	private async Task DeleteAsync(int postId)
	{
		await blogApi.Delete(postId);
		await UpdatePostAsync();
	}
}
