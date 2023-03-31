using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

namespace RiskOfRain2.Bullet
{

	public class Bullet_Base : MonoBehaviour
	{
		[Tooltip("총알의 충돌 처리를 위한 RayCastHit")]
		protected RaycastHit _rayHit = default;

		[SerializeField]
		[Tooltip("총알 충돌 처리를 위한 RayCast 거리")]
		protected float _rayDistance = 0.5f;

		[SerializeField]
		[Tooltip("총알 Rigidbody")]
		protected Rigidbody _bulletRigidbody = default;

		[SerializeField]
		[Tooltip("총알 속도")]
		protected float _speed = 100f;

		[SerializeField]
		[Tooltip("충돌 오브젝트의 위치")]
		protected Vector3 _target;

		protected void Start()
		{
			TryGetComponent(out _bulletRigidbody);
		}

		protected void OnEnable()
		{
			float maxDistance_ = float.MaxValue;
			if (Physics.Raycast(transform.localPosition, transform.forward, out _rayHit, maxDistance_))
			{
				_target = _rayHit.point;
			}
			else
			{
				StartCoroutine(BulletPush());
			}
		}

		IEnumerator BulletPush()
		{
			yield return new WaitForSeconds(5f);
			ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
		}

		protected void FixedUpdate()
		{
			_bulletRigidbody.velocity = transform.forward * _speed;
		}

		protected void Update()
		{
			CollisionCheck();
		}

		protected virtual bool CollisionCheck()
		{
			if (0f < (_target - transform.localPosition).magnitude && (_target - transform.localPosition).magnitude < 1f)
			{
				OnCollision();
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual void OnCollision()
		{
			ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
		}
	}
}