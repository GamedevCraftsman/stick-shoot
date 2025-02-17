using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int _maxtime;
    [Header("Texts")]
    [SerializeField] private Text _timerTxt;
    [SerializeField] private Text _maxTimeTxt;
    [SerializeField] private Text _currentTimeTxt;

    private Coroutine _coroutine;
    private int _currentTime = 0;
    private float _timerDelay = 1;
    public void  Initialize()
    {
       _coroutine = StartCoroutine(StartTimer());
       _maxTimeTxt.text = _maxtime.ToString();
    }

    IEnumerator StartTimer()
    {
        WaitForSeconds _wait = new WaitForSeconds(_timerDelay);

        while (true)
        {
            _currentTime++;
            _timerTxt.text = _currentTime.ToString();
            yield return _wait;
        }
    }

    public void StopTimer()
    {
        StopCoroutine(_coroutine);

        _currentTimeTxt.text = _currentTime.ToString();
    }

    public void ContinueTimer()
    {
		_coroutine = StartCoroutine(StartTimer());
	}

    public bool IsCoughtTime()
    {
        return _currentTime < _maxtime;
    }

    public void ChangeTimerDelay(float currentTimeDelay)
    {
        StopCoroutine(_coroutine);
        _timerDelay = currentTimeDelay;
        ContinueTimer();
    }
}
