using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnShopButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;

    [SerializeField] private Image _image;

    private void OnEnable() => _button.onClick.AddListener(OnClick);
	private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    private void OnClick() => StartCoroutine(ClickFunk());

    IEnumerator ClickFunk()
    {
        MainSceneManager.Instance.PanelsManager.OpenShopPanel();
		yield return new WaitForSeconds(0.5f);
		Click?.Invoke();
	}
}
