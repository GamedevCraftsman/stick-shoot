using System.Collections;
using UnityEngine;

public class MagneticBall : MonoBehaviour
{
	[SerializeField] GameObject _mainBall;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Star"))
		{
			StartCoroutine(MagnateStar(collision.gameObject));
		}
	}

	IEnumerator MagnateStar(GameObject collision)
	{
		WaitForEndOfFrame wait = new WaitForEndOfFrame();
		collision.GetComponent<Animator>().enabled = false;

		while (true)
		{
			if (collision != null)
			{
				collision.transform.position = Vector3.Lerp(collision.transform.position, _mainBall.transform.position, 5 * Time.deltaTime);
				yield return wait;
			}
			else
			{
				break;
			}
		}
	}
}
