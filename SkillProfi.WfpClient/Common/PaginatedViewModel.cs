namespace SkillProfi.WfpClient.Common;

public abstract class PaginatedViewModel : ViewModel
{
	public int PageNumber
	{
		get => _pageNumber;
		set
		{
			int newValue = Math.Clamp(value, 1, TotalPages);

			if (newValue == _pageNumber)
			{
				return;
			}
			
			_pageNumber = value;
			OnPropertyChanged();
			UpdateData();
		}
	}
	
	public int TotalPages
	{
		get => _totalPages;
		set
		{
			_totalPages = value;
			OnPropertyChanged();
		}
	}

	public DelegateCommand NextPageCommand => new(NextPage, CanNextPage);
	public DelegateCommand PreviousPageCommand => new(PreviousPage, CanPreviousPage);
		
	private int _pageNumber = 1;
	private int _totalPages = 1;

	protected abstract void UpdateData();
		
	private void NextPage(object obj)
	{
		PageNumber++;
	}

	private bool CanNextPage(object obj)
	{
		return PageNumber < TotalPages;
	}
	
	private void PreviousPage(object obj)
	{
		PageNumber--;
	}

	private bool CanPreviousPage(object obj)
	{
		return PageNumber > 1;
	}
}
