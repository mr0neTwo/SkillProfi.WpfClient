using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace SkillProfi.WfpClient.Common.UserControls;

public sealed class LoadImageViewModel : ViewModel
{
	public BitmapImage? LoadedImage
	{
		get => _loadedImage; 
		set
		{
			_loadedImage = value;
			ImageBase64 = BitmapImageToBase64(value);
			OnPropertyChanged();
			OnPropertyChanged(nameof(DisplayImage));
		}
	}

	public string DefaultImageUrl
	{
		get => _defaultImageUrl;
		set
		{
			_defaultImageUrl = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(DisplayImage));
		}
	}

	public BitmapImage DisplayImage => GetDisplayedImage();

	public string? ImageBase64 { get; private set; }
	
	public DelegateCommand LoadImageCommand => new(LoadImage);
	
	private BitmapImage? _loadedImage;
	private string _defaultImageUrl = AppVariables.DefaultImagePath;

	public void Clear()
	{
		LoadedImage = null;
		ImageBase64 = null;
		DefaultImageUrl = AppVariables.DefaultImagePath;
	}
	
	private void LoadImage(object parameter)
	{
		OpenFileDialog openFileDialog = new();
		openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

		if (openFileDialog.ShowDialog() == true)
		{
			string filePath = openFileDialog.FileName;
			LoadedImage = UrlToBitmapImage(filePath);
		}
	}

	private BitmapImage GetDisplayedImage()
	{
		if (LoadedImage != null)
		{
			return LoadedImage;
		}

		if (!string.IsNullOrEmpty(DefaultImageUrl))
		{
			return UrlToBitmapImage(DefaultImageUrl);
		}

		return new BitmapImage(); 
	}

	private BitmapImage UrlToBitmapImage(string url)
	{
		BitmapImage bitmap = new();
		bitmap.BeginInit();
		bitmap.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
		bitmap.CacheOption = BitmapCacheOption.OnLoad;
		bitmap.EndInit();
			
		return bitmap;
	}

	private string? BitmapImageToBase64(BitmapImage? bitmapImage)
	{
		if (bitmapImage == null)
		{
			return null;
		}
		
		using MemoryStream memoryStream = new();

		JpegBitmapEncoder encoder = new();
		encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
		encoder.Save(memoryStream);
		byte[] imageBytes = memoryStream.ToArray();
		
		return Convert.ToBase64String(imageBytes);
	}
}
