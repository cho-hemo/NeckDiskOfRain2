using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

namespace RiskOfRain2.Bullet
{

	public class Bullet_Base : MonoBehaviour
	{
		[Tooltip("총알의 충돌 처리를 위한 RayCastHit")]
		protected RaycastHit rayHit = default;

		[SerializeField]
		[Tooltip("사용 스킬의 Index")]
		protected int skillIndex = default;

		[SerializeField]
		[Tooltip("총알 충돌 처리를 위한 RayCast 거리")]
		protected float rayDistance = 0.5f;

		[SerializeField]
		[Tooltip("총알 Rigidbody")]
		protected Rigidbody bulletRigidbody = default;

		[SerializeField]
		[Tooltip("총알 속도")]
		protected float speed = 100f;

		[SerializeField]
		[Tooltip("충돌 오브젝트의 위치")]
		protected Vector3 target;

		protected void Start()
		{
			TryGetComponent(out bulletRigidbody);
		}

		protected void OnEnable()
		{
			float maxDistance_ = float.MaxValue;
			if (Physics.Raycast(transform.localPosition, transform.forward, out rayHit, maxDistance_))
			{
				target = rayHit.point;
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
			bulletRigidbody.velocity = transform.forward * speed;
		}

		protected void Update()
		{
			CollisionCheck();
		}

		/// <summary>
		/// 충돌 체크 함수
		/// </summary>
		/// <returns></returns>
		protected virtual bool CollisionCheck()
		{
			if (0f < (target - transform.localPosition).magnitude && (target - transform.localPosition).magnitude < 2f)
			{
				OnCollision();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 충돌 시 호출되는 함수
		/// </summary>
		public virtual void OnCollision()
		{
			ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
		}

		public virtual void OnHit()
		{

		}
	}
}