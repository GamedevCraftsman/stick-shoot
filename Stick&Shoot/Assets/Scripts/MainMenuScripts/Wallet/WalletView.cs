using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Text _walletText;

    private Wallet _wallet;

	public void Initialized(Wallet wallet)
	{
		_wallet = wallet;

		UpdateValue(_wallet.GetCurrentCoins());

		_wallet.CoinsChanged += UpdateValue;
	}

	private void OnDestroy() => _wallet.CoinsChanged -= UpdateValue;

	private void UpdateValue(int value) => _walletText.text = value.ToString();
}
