using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
	[SerializeField] private StarsManager _starsManager;
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private BallFactory _ballFactory;
	[SerializeField] private Timer _timer;

    private IDataProvider _dataProvider;
    private IPersistentData _dataPlayerData;

	private Wallet _wallet;

	private void Awake()
	{
		InitializeData();

		SpawnBall();

		InitializeTimer();

		InitializeWallet();
	}

	private void SpawnBall()
	{
		GameObject ball = _ballFactory.Get(_dataPlayerData.PlayerData.SelectedBallSkin, _characterSpawnPoint.position);
	}

	private void InitializeData()
	{
		_dataPlayerData = new PersistentData();
		_dataProvider = new DataLocalProvider(_dataPlayerData);

		LoadDataOrInit();
	} 

	private void InitializeWallet()
	{
		_wallet = new Wallet(_dataPlayerData);

		_starsManager.Inialize(_wallet, _dataProvider);	
	}

	private void LoadDataOrInit()
	{
		if(_dataProvider.TryLoad() == false)
			_dataPlayerData.PlayerData = new PlayerData();
	}

	private void InitializeTimer()
	{
		_timer.Initialize();
	}
}
