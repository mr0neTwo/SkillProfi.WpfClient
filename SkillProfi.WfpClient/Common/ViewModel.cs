namespace SkillProfi.WfpClient.Common;

public class ViewModel : ObservableObject
{
	public void BeforeShown()
	{
		OnBeforeShown();
	}

	protected virtual void OnBeforeShown()
	{
	}
}
