using System.Windows.Media;

namespace SkillProfi.WfpClient.Common.UserControls;

public sealed class InputViewModel : ViewModel
{
	public string Label
	{
		get => _label;
		set
		{
			_label = value;
			OnPropertyChanged();
		}
	}

	public string Value
	{
		get => _value;
		set
		{
			_value = value;
			Length = $"{value.Length}/{Limit}";
			OnPropertyChanged();
		}
	}

	public Brush BorderColor
	{
		get => _borderColor;
		set
		{
			_borderColor = value;
			OnPropertyChanged();
		}
	}
	
	public bool Required
	{
		get => _required;
		set
		{
			_required = value;
			OnPropertyChanged();
		}
	}

	public int Width
	{
		get => _width;
		set
		{
			_width = value;
			OnPropertyChanged();
		}
	}

	public int Limit
	{
		get => _limit;
		set
		{
			_limit = value;
			Length = $"{Value.Length}/{value}";
			OnPropertyChanged();
		}
	}

	public bool ShowLimit
	{
		get => _showLimit;
		set
		{
			_showLimit = value;
			OnPropertyChanged();
		}
	}

	public string Length
	{
		get => _length;
		set
		{
			_length = value;
			OnPropertyChanged();
		}
	}

	private string _label = string.Empty;
	private string _value = string.Empty;
	private bool _required;
	private bool _showLimit;
	private int _width = 300;
	private int _limit = int.MaxValue;
	private string _length = string.Empty;
	private Brush _borderColor = Brushes.Gray;
	private bool _isWrong;

	public bool Validate()
	{
		_isWrong = Required && string.IsNullOrWhiteSpace(Value);
		BorderColor = _isWrong ? Brushes.Red : Brushes.Gray;

		return !_isWrong;
	}

	public void Clear()
	{
		Value = string.Empty;
		BorderColor = Brushes.Gray;
		Length = $"{Value.Length}/{Limit}";
	}
}

