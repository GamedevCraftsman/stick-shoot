using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItem;

    [SerializeField] private SpawnShopButton _spawnShopButton;

    [SerializeField] private ShopPanel _shopPanel;

	private void OnEnable()
	{
		_spawnShopButton.Click += OnSpawnSkinsButtonClick;
	}


	private void OnDisable() 
	{
		_spawnShopButton.Click -= OnSpawnSkinsButtonClick; 
	}

	private void OnSpawnSkinsButtonClick()
	{
		_shopPanel.Show(_contentItem.BallsSkins.Cast<BallsSkinsConfigs>());
	}
}
