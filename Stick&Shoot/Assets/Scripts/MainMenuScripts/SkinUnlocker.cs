public class SkinUnlocker : IShopItemVisitor
{
	private IPersistentData _persistentData;

	public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

	public void Visit(BallsSkinsConfigs ballsSkinsConfigs)
		=> _persistentData.PlayerData.OpenBallSkin(ballsSkinsConfigs.SkinType);
}
