namespace SkillProfi.WfpClient.Common;

public abstract class EditorViewModel : ViewModel
{
	public bool ShowCreateButton
	{
		get => _showCreateButton;
		set
		{
			_showCreateButton = value;
			OnPropertyChanged();
		}
	}
	
	public bool ShowSaveButton
	{
		get => _showSaveButton;
		set
		{
			_showSaveButton = value;
			OnPropertyChanged();
		}
	}

	public bool EditMode
	{
		get => _editMode;
		set
		{
			_editMode = value;
			ShowCreateButton = !value;
			ShowSaveButton = value;
		}
	}

	public DelegateCommand CreateCommand => new(Create, CanCreate);
	public DelegateCommand SaveCommand => new(Save, CanSave);
	public DelegateCommand GoBackCommand => new(GoBack, CanGoBack);
	
	protected bool IsLoading;
	private bool _editMode;
	private bool _showCreateButton;
	private bool _showSaveButton;

	protected abstract void Create(object obj);
	
	protected abstract void Save(object obj);
	
	protected abstract void GoBack(object obj);
	
	private bool CanCreate(object obj)
	{
		return !IsLoading;
	}
	
	private bool CanSave(object obj)
	{
		return !IsLoading;
	}
	
	private bool CanGoBack(object obj)
	{
		return !IsLoading;
	}
}
