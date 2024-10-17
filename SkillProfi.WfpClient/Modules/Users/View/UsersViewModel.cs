using System.Collections.ObjectModel;
using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Modules.Users.Models;
using SkillProfi.WfpClient.Services.Navigation;

namespace SkillProfi.WfpClient.Modules.Users.View;

public sealed class UsersViewModel(UsersApi usersApi, INavigationService navigationService, UserEditorViewModel userEditorViewModel) : ViewModel
{
	public ObservableCollection<User> Users { get; set; } = new();
	
	public DelegateCommand CreateCommand => new(Create, CanCreate);
	public DelegateCommand EditCommand => new(Edit, CanEdit);
	public DelegateCommand DeleteCommand => new(Delete, CanDelete);
	
	private bool _isLoading;
	
	protected override void OnBeforeShown()
	{
		_ = UpdateUsersAsync();
	}

	private async Task UpdateUsersAsync()
	{
		List<User> users = await usersApi.GetList() ?? new List<User>();
		Users = new ObservableCollection<User>(users);
		OnPropertyChanged(nameof(Users));
	}

	private void Create(object obj)
	{
		userEditorViewModel.EditMode = false;
		navigationService.NavigateTo<UserEditorViewModel>();
	}
	
	private bool CanCreate(object obj)
	{
		return !_isLoading;
	}
	
	private void Edit(object obj)
	{
		if (obj is User user)
		{
			userEditorViewModel.User = user;
			userEditorViewModel.EditMode = true;
			navigationService.NavigateTo<UserEditorViewModel>();
		}
	}
	
	private bool CanEdit(object obj)
	{
		return !_isLoading;
	}
	
	private void Delete(object obj)
	{
		if (obj is User user)
		{
			_ = DeleteAsync(user.Id);
		}
	}
	
	private bool CanDelete(object obj)
	{
		return !_isLoading;
	}

	private async Task DeleteAsync(int userId)
	{
		_isLoading = true;
		await usersApi.Delete(userId);
		await UpdateUsersAsync();
		_isLoading = false;
	}
}
