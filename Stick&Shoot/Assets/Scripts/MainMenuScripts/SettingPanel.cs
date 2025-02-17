using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] private float _shadowPanelFadeTime = 0.5f;
	[SerializeField] private float _settingsPanelMoveTime = 0.5f;
	[SerializeField] private float _panelsFadeTime = 0.25f;
	[SerializeField] private float _buttonsAppearTime = 0.25f;
	[SerializeField] private Vector2 _moveHidePosition = new Vector2(0, -1250);
	[Header("For fade")]
	[SerializeField] private Image _shadowPanel;
	[SerializeField] private Image _namePanelBack;
	[SerializeField] private Image _namePanel;
	[SerializeField] private Image _settingsPanelBack;
	[Header("For appear")]
	[SerializeField] private GameObject _musicController;
	[SerializeField] private GameObject _soundController;
	[SerializeField] private Transform _closeButton;

	public void OpenSettings()
	{
		ObjectsCancellation();
		PlayApearAnim();
	}

	private void ObjectsCancellation()
	{
		_musicController.transform.localScale = Vector3.zero;
		_soundController.transform.localScale = Vector3.zero;
		_closeButton.transform.localScale = Vector3.zero;
	}

	private void PlayApearAnim()
	{
		Sequence _animation = DOTween.Sequence();

		_animation
			.AppendCallback(() => ButtonState(false))
			.AppendCallback(() => ShadowPanelState(true))
			.Append(_shadowPanel.DOFade(1, _shadowPanelFadeTime))
			.Append(gameObject.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _settingsPanelMoveTime)
				.SetEase(Ease.OutBack))
			.Join(_settingsPanelBack.DOFade(1, _panelsFadeTime))
			.Join(_namePanel.DOFade(1, _panelsFadeTime))
			.Join(_namePanelBack.DOFade(1, _panelsFadeTime))
			.AppendCallback(() => PlaySound(Sounds.ButtonAppear))
			.Append(_musicController.transform.DOScale(1, _buttonsAppearTime).SetEase(Ease.OutBack))
			.AppendCallback(() => PlaySound(Sounds.ButtonAppear))
			.Append(_soundController.transform.DOScale(1, _buttonsAppearTime).SetEase(Ease.OutBack))
			.AppendCallback(() => PlaySound(Sounds.ButtonAppear))
			.Append(_closeButton.DOScale(1, _buttonsAppearTime).SetEase(Ease.OutBack))
			.OnComplete(() => ButtonState(true));
	}

	public void CloseSettings()
	{
		PlayHideAnim();
	}

	private void PlayHideAnim()
	{
		Sequence _animation = DOTween.Sequence();

		_animation
			.AppendCallback(() => ButtonState(false))
			.AppendCallback(() => PlaySound(Sounds.HideButton))
			.Append(_closeButton.DOScale(0, _buttonsAppearTime).SetEase(Ease.InBack))
			.AppendCallback(() => PlaySound(Sounds.HideButton))
			.Append(_soundController.transform.DOScale(0, _buttonsAppearTime).SetEase(Ease.InBack))
			.AppendCallback(() => PlaySound(Sounds.HideButton))
			.Append(_musicController.transform.DOScale(0, _buttonsAppearTime).SetEase(Ease.InBack))
			.Append(gameObject.GetComponent<RectTransform>().DOAnchorPos(_moveHidePosition, _settingsPanelMoveTime)
				.SetEase(Ease.InBack))
			.Append(_shadowPanel.DOFade(0, _shadowPanelFadeTime))
			.Join(_settingsPanelBack.DOFade(0, _panelsFadeTime))
			.Join(_namePanel.DOFade(0, _panelsFadeTime))
			.Join(_namePanelBack.DOFade(0, _panelsFadeTime))
			.OnComplete(() => ShadowPanelState(false));
	}

	private void ButtonState(bool _state)
	{
		_closeButton.GetComponent<Button>().enabled = _state;

	}

	private void ShadowPanelState(bool _state)
	{
		_shadowPanel.gameObject.SetActive(_state);
	}

	private void PlaySound(Sounds playSound)
	{
		SoundManager.Instance.PlaySound(playSound);
	}
}
