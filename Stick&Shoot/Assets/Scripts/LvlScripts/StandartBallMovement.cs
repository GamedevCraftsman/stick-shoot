using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StandartBallMovement : MonoBehaviour
{
	[Header("General")]
	[SerializeField] private string _stickObstacleName;
	[Header("Nitro")]
	[SerializeField] private float _nitroSpeed;
	[SerializeField] private float _nitroCount = 100;
	[SerializeField] private float _nitroSubtractor = 0.2f;
	private Image _nitroViewer;
	[Header("Punch values")]
	[Header("Tap punch")]
	[SerializeField] private float _punchForce;
	//[Header("Start punch")]
	//[SerializeField] private int _fromRandPunchValue = 3;
	//[SerializeField] private int _toRandPunchValue = 7;
	[Header("Rotation values")]
	[SerializeField] private float _normalRotationSpeed;
	[SerializeField] private float orbitRadius = 0.6f;
	[Header("If shield")]
	[SerializeField] private Shield _shield;

	Coroutine _coroutine;
	private Transform _stickObstacle;
	private Rigidbody2D _ballRb;
	private bool _canInteract = false;
	private float _rotationSpeed;
	//private bool _isPunchStart = false;

	private void Start()
	{
		_ballRb = GetComponent<Rigidbody2D>();
		_rotationSpeed = _normalRotationSpeed;
		LvlSceneManager.Instance.StandartBallMovement = this;
		StartCoroutine(BallMovementChecker());
		//RandPunchOnSpawn();
	}

	private void Update()
	{
		Rotate();
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log("Punch");
			MakePunch();
		}
#endif
	}

	//private void RandPunchOnSpawn()
	//{
	//	Vector2 randomDirection = Random.insideUnitCircle.normalized;
	//	float randomSpeed = Random.Range(_fromRandPunchValue, _toRandPunchValue);

	//	GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;
	//}

	private IEnumerator BallMovementChecker()
	{
		WaitForEndOfFrame wait = new WaitForEndOfFrame();

		while (true)
		{
			BallMovement();
			yield return wait;
		}

	}

	private void BallMovement()
	{
		if (Input.touchCount == 1 && _canInteract)
		{
			Punch();
		}
		else if (Input.touchCount == 2 && _canInteract)
		{
			BoostRotate();
		}
	}

	private void Punch()
	{
		Touch touch = Input.GetTouch(0);

		if (touch.phase == TouchPhase.Began)
		{
			MakePunch();
		}
	}

	private void MakePunch()
	{
		SoundManager.Instance.PlaySound(Sounds.BallShoot);

		_ballRb.gravityScale = 1;

		Vector2 _shootDir = (transform.position - _stickObstacle.position).normalized;

		_ballRb.AddForce(_shootDir * _punchForce, ForceMode2D.Impulse);

		_canInteract = false;
	}

	private void BoostRotate()
	{
		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
		_nitroViewer = LvlSceneManager.Instance.NitroViewer;

		if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
		{
			GoNitro();
		}
		else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}

			_rotationSpeed = _normalRotationSpeed;
		}
	}

	private void GoNitro()
	{
		if (_nitroCount > 0)
		{
			_rotationSpeed = _nitroSpeed;

			_coroutine = StartCoroutine(DecreaseNitro());
		}
	}

	IEnumerator DecreaseNitro()
	{
		WaitForEndOfFrame _wait = new WaitForEndOfFrame();

		while (IsNitro())
		{
			_nitroCount -= _nitroSubtractor;
			_nitroViewer.fillAmount = _nitroCount / 100;
			yield return _wait;
		}

		_rotationSpeed = _normalRotationSpeed;
		yield return null;
	}

	private bool IsNitro()
	{
		return _nitroCount > 0;
	}

	private void Rotate()
	{
		if (_canInteract)
		{
			RotateAroundObstacle();
		}
	}

	private void RotateAroundObstacle()
	{
		gameObject.transform.RotateAround(_stickObstacle.position, Vector3.forward, _rotationSpeed * Time.deltaTime);

		KeepDistance();
	}

	private void KeepDistance()
	{
		Vector3 directionFromCenter = (transform.position - _stickObstacle.position).normalized;

		Vector3 targetPosition = _stickObstacle.position + directionFromCenter * orbitRadius;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag(_stickObstacleName))
		{
			SoundManager.Instance.PlaySound(Sounds.StickBall);

			_stickObstacle = collision.gameObject.GetComponent<Transform>();

			_ballRb.velocity = Vector3.zero;
			_ballRb.gravityScale = 0;

			_canInteract = true;
		}
	}

	public void StartPunchChecker()
	{
		StartCoroutine(BallMovementChecker());
	}

	public void StopPunchChecker()
	{
		StopAllCoroutines();
	}

	public bool IsShieldActive()
	{
		if(_shield != null)
		{
			return _shield.IsShieldActive;
		}

		return false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			SoundManager.Instance.PlaySound(Sounds.KickOff);
		}
	}
}
