using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
	public static MainSceneManager Instance;

	[SerializeField] private PanelsManager _panelsManager;

	#region Initialize
	public PanelsManager PanelsManager { get { return _panelsManager; } }
	#endregion

	private void Start()
	{
		Instance = this;

		PanelsManager.HideBlackPanel(StartCoroutine);
	}

	private void StartCoroutine()
	{
		StartCoroutine(StartMusic());
	}

	private IEnumerator StartMusic()
	{
		WaitForNextFrameUnit wait = new WaitForNextFrameUnit();

		yield return wait;

		SoundManager.Instance.PlaySound(Sounds.MenuMusic);
	}
}
