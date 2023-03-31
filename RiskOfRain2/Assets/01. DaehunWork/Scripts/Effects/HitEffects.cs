using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

namespace RiskOfRain2.Effects
{
	public class HitEffects : MonoBehaviour
	{
		#region Inspector
		[SerializeField]
		[Tooltip("오브젝트 Push Delay")]
		private float _pushDelay = 1f;
		#endregion

		#region Property
		public float PushDelay { get { return _pushDelay; } private set { _pushDelay = value; } }
		#endregion

		private void OnEnable()
		{
			StartCoroutine(ObjectPush());
		}

		IEnumerator ObjectPush()
		{
			yield return new WaitForSeconds(1f);
			ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
		}
	}
}

