using UnityEngine;

public class FrameRate : MonoBehaviour
{
	private void Awake()
	{
		Application.targetFrameRate = 120;
	}
}
