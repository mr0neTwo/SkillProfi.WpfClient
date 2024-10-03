using System.ComponentModel;
using System.Windows.Media;

namespace SkillProfi.WfpClient.Common.UserControls;

public class InputViewModel : ViewModel
{
	private string _value;
	private Brush _borderColor = Brushes.Gray;
	private bool _isWrong;

	public string Label { get; set; }

	public string Value
	{
		get => _value;
		set
		{
			_value = value;
			OnPropertyChanged(nameof(Value));
		}
	}

	public bool Required { get; set; }
	public int Limit { get; set; } = int.MaxValue;

	public Brush BorderColor
	{
		get => _borderColor;
		set
		{
			_borderColor = value;
			OnPropertyChanged(nameof(BorderColor));
		}
	}

	public DelegateCommand EnterPressCommand => new(OnEnterPress);
	public DelegateCommand BlurCommand => new(OnBlur);
	

	private void OnEnterPress(object obj)
	{
		Console.WriteLine("Enter нажата!");
	}

	private void OnBlur(object obj)
	{
		Validate();
	}

	public bool Validate()
	{
		_isWrong = Required && string.IsNullOrWhiteSpace(Value);
		BorderColor = _isWrong ? Brushes.Red : Brushes.Gray;

		return !_isWrong;
	}

	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public event PropertyChangedEventHandler PropertyChanged;
}

