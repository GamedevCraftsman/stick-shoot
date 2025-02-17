using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BallFactory", menuName = "ScriptableObjects/BallFactory")]
public class BallFactory : ScriptableObject
{
	[SerializeField] private GameObject _blue;
	[SerializeField] private GameObject _purple;
	[SerializeField] private GameObject _red;
	[SerializeField] private GameObject _yellow;
	[SerializeField] private GameObject _blueLines;
	[SerializeField] private GameObject _blueSpots;
	[SerializeField] private GameObject _stawberry;
	[SerializeField] private GameObject _smile;
	[SerializeField] private GameObject _basketball;
	[SerializeField] private GameObject _football;
	[SerializeField] private GameObject _shield;
	[SerializeField] private GameObject _magnetic;
	[SerializeField] private GameObject _double;
	[SerializeField] private GameObject _nitro;
	[SerializeField] private GameObject _timer;

	public GameObject Get(SkinType skinType, Vector3 spawnPosition)
	{
		GameObject instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity, null);
		//instance.Initialize();

		return instance;
	}

	private GameObject GetPrefab(SkinType skinType)
	{
		switch (skinType)
		{
			case SkinType.Blue:
				return _blue;
			case SkinType.Purple:
				return _purple;
			case SkinType.Red:
				return _red;
			case SkinType.Yellow:
				return _yellow;
			case SkinType.BlueLines:
				return _blueLines;
			case SkinType.BlueSpots:
				return _blueSpots;
			case SkinType.Strawberry:
				return _stawberry;
			case SkinType.Smile:
				return _smile;
			case SkinType.Basketball:
				return _basketball;
			case SkinType.Football:
				return _football;
			case SkinType.Shield:
				return _shield;
			case SkinType.Magnetic:
				return _magnetic;
			case SkinType.Double:
				return _double;
			case SkinType.Nitro:
				return _nitro;
			case SkinType.Timer:
				return _timer;

			default:
				throw new ArgumentException(nameof(skinType));
		}
	}
}
