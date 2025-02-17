using UnityEngine;

public class SlowedTime : MonoBehaviour
{
    [SerializeField] private float _currentTimeDelay;

	private void Start()
	{
		LvlSceneManager.Instance.Timer.ChangeTimerDelay(_currentTimeDelay);
	}
}
