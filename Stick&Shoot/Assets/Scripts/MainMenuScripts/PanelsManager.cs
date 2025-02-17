#nullable enable
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelsManager : MonoBehaviour
{
    [Header("GENERAL")]

    [Header("Game objects")]
    [SerializeField] private Image _blackPanel;
    [SerializeField] private RectTransform _exitButton;

    [Header("Values")]
    [Header("Black panel")]
    [SerializeField] private float _defaultBlackPanelTransparency = 0;
	[SerializeField] private float _showBlackPanelTransparency = 1;
    [SerializeField] private float _blackPanelFadeTime = 0.5f;
    [Header("Exit button")]
    [SerializeField] private float _exitBtnShowTime = 0.5f;
    [SerializeField] private Vector2 _showMovePositionExitBtn = new Vector2(115, -250);
	[SerializeField] private Vector2 _hideMovePositionExitBtn = new Vector2(115, 150);

	[Header("PANELS")]

	[Header("Lvl Panel")]
    [SerializeField] private GameObject _lvlPanel;
	[Header("ShopPanel")]
	[SerializeField] private GameObject _shopPanel;

	#region PanelsNames
	const string _lvlPanelName = "Lvl";
	const string _shopPanelName = "Shop";
	#endregion

	#region PrivateValues
	private string? _currentPanelName;
	#endregion

	#region General
	private Tween ShowExitButton()
    {
        return _exitButton.DOAnchorPos(_showMovePositionExitBtn, _exitBtnShowTime).SetEase(Ease.OutBack);
	}

	private Tween HideExitButton()
	{
		return _exitButton.DOAnchorPos(_hideMovePositionExitBtn, _exitBtnShowTime).SetEase(Ease.InBack);
	}

    private void OpenPanelAnim(Action _panelFunc)
    {
		Sequence _animation = DOTween.Sequence();
		_blackPanel.gameObject.SetActive(true);
		_exitButton.gameObject.GetComponent<Button>().enabled = false;

		_animation
			.Append(_blackPanel.DOFade(_showBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => _panelFunc?.Invoke())
			.Append(_blackPanel.DOFade(_defaultBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => _blackPanel.gameObject.SetActive(false))
			.Join(ShowExitButton())
			.OnComplete(() => _exitButton.gameObject.GetComponent<Button>().enabled = true);
	}

    public void CloseCurrentPanel()
    {
		Action _panelFunc = SetClosePanel();
		CloseAnim(_panelFunc);
		ValueCancellation();
	}

	private Action SetClosePanel()
	{
		Action? _panelFunc = null;
		switch (_currentPanelName)
		{
			case _lvlPanelName:
				_panelFunc = () => SetLvlPanelState(false);
				break;
			case _shopPanelName:
				_panelFunc = () => SetShopPanelState(false);
				break;
			default:
				Debug.LogWarning("Didn’t find panel’s name!");
				break;
		}

		return _panelFunc;
	}

	private void CloseAnim(Action _panelFunc)
	{
		Sequence _animation = DOTween.Sequence();
		_blackPanel.gameObject.SetActive(true);
		_exitButton.gameObject.GetComponent<Button>().enabled = false;

		_animation
			.Append(HideExitButton())
			.Append(_blackPanel.DOFade(_showBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => _panelFunc?.Invoke())
			.Append(_blackPanel.DOFade(_defaultBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => _blackPanel.gameObject.SetActive(false));
	}

	private void ValueCancellation()
	{
		_currentPanelName = "";
	}
	#endregion

	#region LvlPanel
	public void OpenLvlPanel()
    {
		OpenPanelAnim(() => SetLvlPanelState(true));
		_currentPanelName = _lvlPanelName; //Set name of current panel.
	}

	private void SetLvlPanelState(bool _state)
	{
		_lvlPanel.SetActive(_state);
	}
	#endregion

	#region ShopPanel
	public void OpenShopPanel()
	{
		OpenPanelAnim(() => SetShopPanelState(true));
		_currentPanelName = _shopPanelName; //Set name of current panel.
	}

	private void SetShopPanelState(bool _state)
	{
		_shopPanel.SetActive(_state);
	}
	#endregion

	#region Quit
	public void Quit()
	{
		Application.Quit();
	}
	#endregion

	#region Show/Hide Balack Panel
	public void ShowBlackPanel(Action? playFunc)
	{
		Sequence _animation = DOTween.Sequence();
		_blackPanel.gameObject.SetActive(true);

		_animation
			.Append(_blackPanel.DOFade(_showBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => playFunc?.Invoke());
	}

	public void HideBlackPanel(Action? playFunc)
	{
		Sequence _animation = DOTween.Sequence();
		_blackPanel.gameObject.SetActive(true);

		_animation
			.Append(_blackPanel.DOFade(_defaultBlackPanelTransparency, _blackPanelFadeTime))
			.AppendCallback(() => _blackPanel.gameObject.SetActive(false))
			.AppendCallback(() => playFunc?.Invoke());
	}
	#endregion
}
