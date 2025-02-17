using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContainer", menuName = "ScriptableObjects/ShopContainer")]
public class ShopContent : ScriptableObject
{
	[SerializeField] private List<BallsSkinsConfigs> _ballsSkinsConfigs;

	public IEnumerable<BallsSkinsConfigs> BallsSkins => _ballsSkinsConfigs;
}
