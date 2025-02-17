using UnityEngine;

public class Star : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			CollectStar(1);
		}
		else if (collision.CompareTag("DoubleBall"))
		{
			Debug.Log("Add x2");
			CollectStar(2);
		}
	}

	private void CollectStar(int stars)
	{
		SoundManager.Instance.PlaySound(Sounds.StarCollect);

		LvlSceneManager.Instance.StarsManager.SpawnParticles(transform);

		LvlSceneManager.Instance.StarsManager.AddStars(stars);

		Destroy(gameObject);
	}
}
