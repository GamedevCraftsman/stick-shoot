using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndLvlManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] private UnityEvent _changeScene;
	[SerializeField] private GameObject _darkpanel;
	[SerializeField] private GameObject _generalPanel;
	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _losePanel;
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private int _currentLvl = 1;
	[Header("Lose")]
	[SerializeField] private GameObject _loseName;
	[SerializeField] private GameObject[] _loseButtons;
	[Header("Win")]
	[SerializeField] private GameObject _winName;
	[SerializeField] private GameObject[] _winButtons;

	private string _doneLvls;
	private string _openLvls;
	private void Start()
	{
		_openLvls = PlayerPrefs.GetString("OpenLvls");
		_doneLvls = PlayerPrefs.GetString("DoneLvls");

	}

	public void SetWin()
	{
		SoundManager.Instance.PlaySound(Sounds.Win);

		_winPanel.SetActive(true);
		LvlSceneManager.Instance.StarsManager.SummingStars();
		AppearPanel(_winName, _winButtons);

		SaveLvl();
	}

	public void SetLose()
	{
		SoundManager.Instance.PlaySound(Sounds.Lose);

		_losePanel.SetActive(true);
		LvlSceneManager.Instance.StarsManager.SummingStars();
		AppearPanel(_loseName, _loseButtons);
	}

	private void AppearPanel(GameObject panelName, GameObject[] buttons)
	{
		LvlSceneManager.Instance.Timer.StopTimer();
		_darkpanel.SetActive(true);
		_generalPanel.SetActive(true);

		Sequence _animation = DOTween.Sequence();

		_animation
			.Append(_generalPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
			.Join(panelName.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));

		StartCoroutine(AppearButtons(buttons));
	}

	private IEnumerator AppearButtons(GameObject[] buttons)
	{
		yield return new WaitUntil(IsEndAnimation);

		Sequence _animation = DOTween.Sequence();
		int buttonsCount = buttons.Length;

		for (int i = 0; i < buttons.Length; i++)
		{

			int index = i;

			_animation
				.AppendCallback(() => SoundManager.Instance.PlaySound(Sounds.ButtonAppear))
				.AppendCallback(() => ChangeButtonState(buttonsCount, index, buttons[index], false))
				.Append(buttons[i].transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
				.AppendCallback(() => ChangeButtonState(buttonsCount, index, buttons[index], true));
		}
	}

	private void ChangeButtonState(int buttonsCount, int buttonNumber, GameObject button, bool state)
	{
		if (buttonNumber < buttonsCount)
		{
			button.GetComponent<Button>().enabled = state;
		}
	}

	private bool IsEndAnimation()
	{
		if (DOTween.TotalPlayingTweens() == 0) return true;
		else return false;
	}

	public void SpawnDieParticles(Transform spawnPoint)
	{
		_particleSystem.transform.position = spawnPoint.position;
		_particleSystem.Play();
	}

	private void SaveLvl()
	{
		_doneLvls = _doneLvls + _currentLvl.ToString();
		Debug.Log($"Open lvls beforeAdded: {_openLvls}");

		_openLvls = _openLvls + (_currentLvl + 1).ToString();

		Debug.Log($"Open lvls afterAdded: {_openLvls}");

		PlayerPrefs.SetString("OpenLvls", _openLvls);
		PlayerPrefs.SetString("DoneLvls", _doneLvls);
	}

	public void LvlButton()
	{
		int isLvlPanelOpen = 1;

		PlayerPrefs.SetInt("isLvlPanelOpen", isLvlPanelOpen);

		LvlSceneManager.Instance.PanelsManager.ShowBlackPanel(ChangeScene);	
	}

	private void ChangeScene()
	{
		_changeScene?.Invoke();
	}
}
