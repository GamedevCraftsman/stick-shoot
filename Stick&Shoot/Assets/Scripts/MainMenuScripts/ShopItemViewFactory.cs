using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ShopItemViewFactory", menuName ="ScriptableObjects/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
	[SerializeField] private ShopItemView _ballSkinPrefabs;

	public ShopItemView Get(BallsSkinsConfigs skin, Transform parent)
	{
		ShopItemVisitor visitor = new ShopItemVisitor(_ballSkinPrefabs);
		visitor.Visit(skin);

		ShopItemView instance = Instantiate(visitor.Prefab, parent);
		instance.Initialization(skin);

		return instance;
	}

	private class ShopItemVisitor : IShopItemVisitor
	{
		private ShopItemView _ballSkinItemPrefab;

		public ShopItemVisitor(ShopItemView ballSkin) => _ballSkinItemPrefab = ballSkin;

		public ShopItemView Prefab { get; private set; }

		public void Visit(BallsSkinsConfigs ballSkin)
			=> Prefab = _ballSkinItemPrefab;
	}
}
