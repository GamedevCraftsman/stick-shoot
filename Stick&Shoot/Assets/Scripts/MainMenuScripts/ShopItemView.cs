using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockContentImage;
    [SerializeField] private Text _costText;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private Sprite _standartBackground;

    private Image _backgroundImage; // for highlight.

    public BallsSkinsConfigs BallsSkinsConfigs { get; private set; }
    public bool IsLock { get; private set; }

    public int Price => BallsSkinsConfigs.SkinPrice;
    public GameObject Model => BallsSkinsConfigs.BallPrefab;

    public void Initialization(BallsSkinsConfigs ballsSkinsConfigs)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        BallsSkinsConfigs = ballsSkinsConfigs;

        _costText.text = ballsSkinsConfigs.SkinPrice.ToString();
        _contentImage.sprite = ballsSkinsConfigs.SkinPicture;
        _lockContentImage.sprite = ballsSkinsConfigs.SkinPicture;
		// add price.
	}

    public void Lock()
    {
        IsLock = true;
		_contentImage.gameObject.SetActive(!IsLock);
		_lockImage.gameObject.SetActive(IsLock);
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        _contentImage.gameObject.SetActive(!IsLock);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    //make select function.
    //make unselect function.
}
