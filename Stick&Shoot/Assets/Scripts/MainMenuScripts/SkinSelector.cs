public class SkinSelector : IShopItemVisitor
{
	private IPersistentData _persistentData;

	public bool IsSelected { get; private set; }

	public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

	public void Visit(BallsSkinsConfigs ballsSkinsConfigs)
		=> _persistentData.PlayerData.SelectedBallSkin = ballsSkinsConfigs.SkinType;
}
