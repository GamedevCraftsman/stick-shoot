using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
	private IPersistentData _persistentData;

	public bool IsOpened {  get; private set; }

	public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

	public void Visit(BallsSkinsConfigs ballsSkinsConfigs)
		=> IsOpened = _persistentData.PlayerData.OpenBallsSkins.Contains(ballsSkinsConfigs.SkinType);
}
