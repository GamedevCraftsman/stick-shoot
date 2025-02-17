using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	[SerializeField] private GameObject _pausePanel;
	[SerializeField] private GameObject[] _pauseButtons;

	public void OpenPause()
	{
		OpenAnimatioin();
	}

	private void OpenAnimatioin()
	{
		LvlSceneManager.Instance.Timer.StopTimer();
		LvlSceneManager.Instance.StandartBallMovement.StopPunchChecker();
		Sequence _animation = DOTween.Sequence();
		_pausePanel.SetActive(true);

		for (int i = 0; i < _pauseButtons.Length; i++)
		{
			ChangeButtonState(false);

			_animation
				.AppendCallback(() => SoundManager.Instance.PlaySound(Sounds.ButtonAppear))
				.Append(_pauseButtons[i].transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack));		
		}

		StartCoroutine(OpenedPause());
	}

	private IEnumerator OpenedPause()
	{
		yield return new WaitUntil(IsEndAllTweens);
		ChangeButtonState(true);
	}

	private void ChangeButtonState(bool state)
	{
		for (int i = 0; i < _pauseButtons.Length; i++)
		{
			_pauseButtons[i].GetComponent<Button>().enabled = state;
		}
	}

	private bool IsAllButtonsUsed(int buttonNumber)
	{
		return buttonNumber < _pauseButtons.Length;
	}

	public void ResumeGame()
	{
		CloseAnimation();
	}

	private void CloseAnimation()
	{
		Sequence _animation = DOTween.Sequence();

		for (int i = 0; i < _pauseButtons.Length; i++)
		{
			ChangeButtonState(false);
			_animation
				.AppendCallback(() => SoundManager.Instance.PlaySound(Sounds.HideButton))
				.Append(_pauseButtons[i].transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
		}

		StartCoroutine(ClosePause());
	}

	IEnumerator ClosePause()
	{
		yield return new WaitUntil(IsEndAllTweens);
		ChangeButtonState(true);
		LvlSceneManager.Instance.Timer.ContinueTimer();
		LvlSceneManager.Instance.StandartBallMovement.StartPunchChecker();
		_pausePanel.SetActive(false);
	}

	private bool IsEndAllTweens()
	{
		if (DOTween.TotalPlayingTweens() == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
