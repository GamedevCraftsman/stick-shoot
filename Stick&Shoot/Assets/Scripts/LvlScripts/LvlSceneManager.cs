using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LvlSceneManager : MonoBehaviour
{
    public static LvlSceneManager Instance;

	[SerializeField] private StarsManager _starsManager;
	[SerializeField] private EndLvlManager _endLvlManager;
	[SerializeField] private Pause _pause;
	[SerializeField] private Image _nitroViewer;
	[SerializeField] private Timer _timer;
	[SerializeField] private PanelsManager _panelsManager;
	
	#region initialize
	public Pause Pause {  get { return _pause; } }
	public Image NitroViewer {  get { return _nitroViewer; } }
	public Timer Timer { get { return _timer; } }
	public EndLvlManager EndLvlManager {  get { return _endLvlManager; } }
	public StarsManager StarsManager { get { return _starsManager; } }
	public StandartBallMovement StandartBallMovement { get; set; }
	public PanelsManager PanelsManager { get { return _panelsManager; } }
	#endregion

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_panelsManager.HideBlackPanel(StartCoroutine);
	}

	private void StartCoroutine()
	{
		StartCoroutine(StartMusic());
	}

	private IEnumerator StartMusic()
	{
		WaitForNextFrameUnit wait = new WaitForNextFrameUnit();

		yield return wait;
		SoundManager.Instance.PlaySound(Sounds.GameMusic);
	}
}
