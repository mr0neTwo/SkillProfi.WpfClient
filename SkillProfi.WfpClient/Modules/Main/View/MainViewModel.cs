using SkillProfi.WfpClient.Common;
using SkillProfi.WfpClient.Common.UserControls;

namespace SkillProfi.WfpClient.Modules.Main.View;

public sealed class MainViewModel(SiteItemApi siteItemApi) : ViewModel
{
	private const string TitleQuote = "TitleQuote";
	private const string MainQuote = "MainQuote";
	private const string Messages = "Messages";
	private const string Main = "Main";
	private const string Services = "Services";
	private const string Projects = "Projects";
	private const string Blog = "Blog";
	private const string Contacts = "Contacts";
	private const string Users = "Users";

	public Dictionary<string, string> SiteItems 
	{ 
		get => _siteItems; 
		set => _siteItems = value;
	}
	
	public InputViewModel TitleQuoteInput { get; set; } = new()
	{
		Label = "Цитата шапки",
		Width = 600,
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel MainQuoteInput { get; set; } = new()
	{
		Label = "Цитата главной страницы",
		Width = 600,
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel MessagesInput { get; set; } = new()
	{
		Label = "Меню: Обращения",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel MainInput { get; set; } = new()
	{
		Label = "Меню: Главная",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel ServicesInput { get; set; } = new()
	{
		Label = "Меню: Услуги",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel ProjectsInput { get; set; } = new()
	{
		Label = "Меню: Проекты",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel BlogInput { get; set; } = new()
	{
		Label = "Меню: Блог",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel ContactsInput { get; set; } = new()
	{
		Label = "Меню: Контакты",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};
	
	public InputViewModel UsersInput { get; set; } = new()
	{
		Label = "Меню: Пользователи",
		Limit = FieldLimits.SiteItemTitleMaxLength
	};

	public DelegateCommand SaveCommand => new(Save, CanSave);

	private Dictionary<string, string> _siteItems = new();
	private Dictionary<string, string> _siteItemToUpdate = new();
	private bool _isLoading;

	protected override void OnBeforeShown()
	{
		_ = UpdateSiteItems();
	}

	private async Task UpdateSiteItems()
	{
		SiteItems = await siteItemApi.GetAllAsync() ?? new Dictionary<string, string>();
		
		UpdateField(TitleQuote, TitleQuoteInput);
		UpdateField(MainQuote, MainQuoteInput);
		UpdateField(Messages, MessagesInput);
		UpdateField(Main, MainInput);
		UpdateField(Services, ServicesInput);
		UpdateField(Projects, ProjectsInput);
		UpdateField(Blog, BlogInput);
		UpdateField(Contacts, ContactsInput);
		UpdateField(Users, UsersInput);
	}

	private void UpdateField(string key, InputViewModel input)
	{
		if (SiteItems.TryGetValue(key, out string? title))
		{
			input.Value = title;
		}
	}

	private void Save(object obj)
	{
		_ = SaveAsync();
	}

	private async Task SaveAsync()
	{
		_siteItemToUpdate.Clear();

		CheckAndUpdate(TitleQuote, TitleQuoteInput);
		CheckAndUpdate(MainQuote, MainQuoteInput);
		CheckAndUpdate(Messages, MessagesInput);
		CheckAndUpdate(Main, MainInput);
		CheckAndUpdate(Services, ServicesInput);
		CheckAndUpdate(Projects, ProjectsInput);
		CheckAndUpdate(Blog, BlogInput);
		CheckAndUpdate(Contacts, ContactsInput);
		CheckAndUpdate(Users, UsersInput);

		if (_siteItemToUpdate.Count == 0)
		{
			return;
		}
		
		_isLoading = true;
		await siteItemApi.UpdateAllAsync(_siteItemToUpdate);
		await UpdateSiteItems();
		_isLoading = false;
	}

	void CheckAndUpdate(string key, InputViewModel input)
	{
		if (SiteItems.TryGetValue(key, out string? currentValue) && input.Value != currentValue)
		{
			_siteItemToUpdate.Add(key, input.Value);
		}
	}

	private bool CanSave(object obj)
	{
		return !_isLoading;
	}
}
