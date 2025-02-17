
public class SelectedSkinChecker : IShopItemVisitor
{
	private IPersistentData _persistentData;

	public bool IsSelected { get; private set; }

	public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

	public void Visit(BallsSkinsConfigs ballsSkinsConfigs)
		=> IsSelected = _persistentData.PlayerData.SelectedBallSkin == ballsSkinsConfigs.SkinType;
}
