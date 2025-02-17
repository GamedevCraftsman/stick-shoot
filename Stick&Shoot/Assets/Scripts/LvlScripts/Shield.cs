using UnityEngine;

public class Shield : MonoBehaviour
{
	[SerializeField] private GameObject _mainBall;

	public bool IsShieldActive { get { return true; } set { } }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("DangerObstacle"))
		{
			SoundManager.Instance.PlaySound(Sounds.DestroyObstacle);

			_mainBall.GetComponent<Rigidbody2D>().velocity = new Vector3(_mainBall.GetComponent<Rigidbody2D>().velocity.x, 0, 0);
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
