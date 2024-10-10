namespace SkillProfi.WfpClient.Common;

public abstract class ViewModel : ObservableObject
{
	public void BeforeShown()
	{
		OnBeforeShown();
	}

	protected virtual void OnBeforeShown()
	{
	}
}