using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

	[SerializeField] private Image _ballPreview;
	[SerializeField] private Transform _itemParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    [SerializeField] private PanelsManager panelsManager;

	private IDataProvider _dataProvider;

	private ShopItemView _previewedItem;
	private Wallet _wallet;

	private SkinUnlocker _skinUnlocker;
	private SelectedSkinChecker _selectedSkinChecker;
	private OpenSkinsChecker _openSkinsChecker;
	private SkinSelector _skinSelector;

	public void Initialize(IDataProvider dataProvider,Wallet wallet, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinCheker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _wallet = wallet;
		_openSkinsChecker = openSkinsChecker;
		_selectedSkinChecker = selectedSkinCheker;
		_skinSelector = skinSelector;
		_skinUnlocker = skinUnlocker;

		_dataProvider = dataProvider;
	}

    public void Show(IEnumerable<BallsSkinsConfigs> items)
    {
        Clear();

        foreach (BallsSkinsConfigs ball in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(ball, _itemParent);

            spawnedItem.Click += OnItemViewClick;

            _openSkinsChecker.Visit(spawnedItem.BallsSkinsConfigs);

            if (_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItem.BallsSkinsConfigs);

                if (_selectedSkinChecker.IsSelected)
                {
                    Debug.Log($"Selected gameobject: {spawnedItem.gameObject.name}");
					_ballPreview.sprite = spawnedItem.BallsSkinsConfigs.SkinPicture;
					ItemViewClicked?.Invoke(spawnedItem);
                }

                spawnedItem.Unlock();
            }
			else
			{
				spawnedItem.Lock();

		    }

			_shopItems.Add(spawnedItem);
        }
	}

    private void OnItemViewClick(ShopItemView item)
	{
		_previewedItem = item;

		_openSkinsChecker.Visit(_previewedItem.BallsSkinsConfigs);

		if (_openSkinsChecker.IsOpened)
		{
			_selectedSkinChecker.Visit(_previewedItem.BallsSkinsConfigs);

			if (_selectedSkinChecker.IsSelected)
			{
				return;
			}
			else
			{
				SoundManager.Instance.PlaySound(Sounds.ButtonClick);

				SelectSkin();
				return;
			}
		}
		else
		{
			Debug.Log("Buy skin");
			OnBuyButtonClick();
		}
	}

	private void SelectSkin()
	{
		_skinSelector.Visit(_previewedItem.BallsSkinsConfigs);
		_ballPreview.sprite = _previewedItem.BallsSkinsConfigs.SkinPicture;
		_dataProvider.Save();
	}

	private void OnBuyButtonClick()
	{
		if (_wallet.IsEnogh(_previewedItem.Price))
		{
			SoundManager.Instance.PlaySound(Sounds.BuySound);

			_wallet.Spend(_previewedItem.Price);

			_skinUnlocker.Visit(_previewedItem.BallsSkinsConfigs);

			SelectSkin();

			_previewedItem.Unlock();
			_dataProvider.Save();
		}
		else
		{
			Debug.Log("Don`t enought money");
		}
	}

	private void Clear()
    {
        foreach (ShopItemView spawnedItem in _shopItems)
        {
            Destroy(spawnedItem.gameObject);
        }

        _shopItems.Clear();
    }
}
