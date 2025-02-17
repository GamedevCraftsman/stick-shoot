using DG.Tweening;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball") || collision.CompareTag("DoubleBall"))
		{
			DecreaceBallSize(collision.gameObject);
			EndLvl();
		}
	}

	private void EndLvl()
	{
		if (LvlSceneManager.Instance.Timer.IsCoughtTime())
		{
			LvlSceneManager.Instance.EndLvlManager.SetWin();
		}
		else
		{
			LvlSceneManager.Instance.EndLvlManager.SetLose();
		}
	}

	private void DecreaceBallSize(GameObject ball)
	{
		ball.GetComponent<CircleCollider2D>().enabled = false;
		ball.transform.DOScale(0f, 0.1f).SetEase(Ease.InCirc);
	}
}
