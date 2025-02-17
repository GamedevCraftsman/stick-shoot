using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private ShopPanel _shopPanel;
	[SerializeField] private WalletView _walletView;
	[SerializeField] private LockLvlsChecker _lockLvlsChecker;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private Wallet _wallet;

	public void Awake()
	{
		//PlayerPrefs.DeleteAll();
		//Debug.Log($"Persistent Data Path: {Application.persistentDataPath}");

		InitializeData();

		InitializeWallet();

		InitializeShop();

		InitializeLvls();
	}

	private void InitializeData()
	{
		_persistentPlayerData = new PersistentData();
		_dataProvider = new DataLocalProvider(_persistentPlayerData);

		LoadDataOrInit();
	}

	private void InitializeWallet()
	{
		_wallet = new Wallet(_persistentPlayerData);

		_walletView.Initialized(_wallet);
	}

	private void InitializeShop()
	{
		OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
		SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
		SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
		SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);

		_shopPanel.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
	}

	private void InitializeLvls()
	{
		_lockLvlsChecker.Initialize();
	}

	private void LoadDataOrInit()
	{
		if (_dataProvider.TryLoad() == false)
			_persistentPlayerData.PlayerData = new PlayerData();
	}
}
