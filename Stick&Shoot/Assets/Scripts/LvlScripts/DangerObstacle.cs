using UnityEngine;

public class DangerObstacle : MonoBehaviour
{ 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball") || collision.CompareTag("DoubleBall"))
		{
			if (collision.GetComponent<StandartBallMovement>() != null && !collision.GetComponent<StandartBallMovement>().IsShieldActive())
			{
				//Debug.Log("IsShield: " +  collision.GetComponent<StandartBallMovement>().IsShieldActive());
				SoundManager.Instance.PlaySound(Sounds.BallDestroy);
				Destroy(collision.gameObject);
				LvlSceneManager.Instance.EndLvlManager.SetLose();
				LvlSceneManager.Instance.EndLvlManager.SpawnDieParticles(collision.transform);
			}
		}
	}
}
