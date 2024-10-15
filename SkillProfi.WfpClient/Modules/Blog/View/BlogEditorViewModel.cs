using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;
using SkillProfi.WfpClient.Modules.Blog.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Blog.View;

public sealed class BlogEditorViewModel(BlogApi blogApi, INavigationService navigationService) : EditorViewModel
{
	public Post Post
	{
		get => _post;
		set
		{
			_post = value;
			TitleInput.Value = value.Title;
			DescriptionInput.Value = value.Description;
			ImageLoader.DefaultImageUrl = value.FullImageUrl;
		}
	}
	
	public LoadImageViewModel ImageLoader { get; set; } = new();

	public InputViewModel TitleInput { get; set; } = new InputViewModel
	{
		Label = "Заголовок", 
		Required = true, 
		Limit = FieldLimits.PostTitleMaxLength, 
	};

	public InputViewModel DescriptionInput { get; set; } = new InputViewModel
	{
		Label = "Описание", 
		Required = true, 
		Limit = FieldLimits.PostDescriptionMaxLength, 
		Width = 600,
		ShowLimit = true
	};
	
	private Post _post = new();
	

	protected override void OnBeforeShown()
	{
		if (!EditMode)
		{
			ClearForms();
		}
	}

	private void ClearForms()
	{
		TitleInput.Clear();
		DescriptionInput.Clear();
		ImageLoader.Clear();
	}

	protected override void Create(object obj)
	{
		if (Validate())
		{
			_ = CreateAsync();
		}
	}

	private async Task CreateAsync()
	{
		CreatePostDto createPostDto = new()
		{
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value, 
			ImageBase64 = ImageLoader.ImageBase64
		};

		IsLoading = true;
		await blogApi.Create(createPostDto);
		IsLoading = false;
		navigationService.NavigateTo<BlogViewModel>();
	}

	protected override void Save(object obj)
	{
		if (Validate())
		{
			_ = SaveAsync();
		}
	}

	private async Task SaveAsync()
	{
		UpdatePostDto updatePostDto = new()
		{
			Id = Post.Id, 
			Title = TitleInput.Value, 
			Description = DescriptionInput.Value, 
			ImageBase64 = ImageLoader.ImageBase64
		};
		
		IsLoading = true;
		await blogApi.Update(updatePostDto);
		IsLoading = false;
		navigationService.NavigateTo<BlogViewModel>();
	}

	protected override void GoBack(object obj)
	{
		navigationService.NavigateTo<BlogViewModel>();
	}

	private bool Validate()
	{
		return DescriptionInput.Validate() && TitleInput.Validate();
	}
}
