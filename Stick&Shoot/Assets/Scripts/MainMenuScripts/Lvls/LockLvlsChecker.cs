using System.Collections;
using UnityEngine;

public class LockLvlsChecker : MonoBehaviour
{
    [SerializeField] private NumberOfLvl[] _lockPanelsNumbers;

	string _openLvls;
	string _doneLvls;

	private void Start()
	{
		LvlPanelChecker();
	}

	public void Initialize()
	{
		_openLvls = PlayerPrefs.GetString("OpenLvls");
		_doneLvls = PlayerPrefs.GetString("DoneLvls");
		Debug.Log($"Open Lvls before add: {_openLvls}\nDone lvls: {_doneLvls}");

		AddStartlvl();

		Debug.Log($"Open Lvls: {_openLvls}\nDone lvls: {_doneLvls}");
	}

	private void AddStartlvl()
	{
		if (_openLvls == "")
		{
			_openLvls += 1;
		}
	}

	public void OpenLvlPanel()
    {
		StartCoroutine(SpawnOpenPanels());
    }

	private IEnumerator SpawnOpenPanels()
	{
		MainSceneManager.Instance.PanelsManager.OpenLvlPanel();
		yield return new WaitForSeconds(0.5f);
		CheckOpenPanel();
	}

	private void CheckOpenPanel()
	{
		for (int i = 0; i < _lockPanelsNumbers.Length; i++)
		{
			UnlockLvls(i);

			if (IsDoneLvls())
			{
				MarkDoneLvls(i);
			}
		}
	}

	private void UnlockLvls(int i)
	{
		for (int j = 0; j < _openLvls.Length; j++)
		{
			if (_lockPanelsNumbers[i].LvlNumber.ToString() == _openLvls[j].ToString())
			{
				_lockPanelsNumbers[i].LockPanel.SetActive(false);
			}
		}
	}

	private bool IsDoneLvls()
	{
		return _doneLvls != null;
	}

	private void MarkDoneLvls(int i)
	{
		for (int j = 0; j < _doneLvls.Length; j++)
		{
			if (_lockPanelsNumbers[i].LvlNumber.ToString() == _doneLvls[j].ToString())
			{
				_lockPanelsNumbers[i].DonePanel.SetActive(true);
				_lockPanelsNumbers[i].LockPanel.SetActive(false);
				_lockPanelsNumbers[i].LvlNumberObj.SetActive(false);
			}
		}
	}

	private void LvlPanelChecker()
	{
		int isLvlPanelOpen = 0;
		isLvlPanelOpen = PlayerPrefs.GetInt("isLvlPanelOpen");

		if (isLvlPanelOpen == 1)
		{
			OpenLvlPanel();

			isLvlPanelOpen = 0;
			PlayerPrefs.SetInt("isLvlPanelOpen", isLvlPanelOpen);
		}
	}
}