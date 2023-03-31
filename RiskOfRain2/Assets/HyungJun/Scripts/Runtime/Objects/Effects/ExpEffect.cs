using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

public class ExpEffect : MonoBehaviour
{
	private Transform _playerTransform = default;

	private void OnEnable()
	{
		// 플레이어 트렌스폼 초기화
		_playerTransform = GameManager.Instance.PlayerTransform;


		StartCoroutine(ActiveEffect());
	}

	// 플레이어를 따라가는 로직
	private IEnumerator ActiveEffect()
	{
		float random = Random.Range(-0.001f, 0.001f);
		float random2 = Random.Range(0.0001f, 0.001f);
		float value = 0f;
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			transform.position = Vector3.Lerp(transform.position + new Vector3(random, random2, 0) * 20,
				 _playerTransform.position + Vector3.up, EaseInCubic(0f, 1f, value));


			value += 0.01f / 2;
		}
	}

	private float EaseInCubic(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value + start;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			gameObject.SetActive(false);
			// 사라지면서 오브젝트 풀에 넣어주는 로직 작성중
			ObjectPoolManager.Instance.ObjectPoolPush("ExpEffect", gameObject);
		}
	}
}
