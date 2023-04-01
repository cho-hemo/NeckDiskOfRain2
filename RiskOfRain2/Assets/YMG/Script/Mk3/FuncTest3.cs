using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncTest3 : MonoBehaviour
{
	private Transform Target;

	public GameObject bulletPrefab;
	private float spawnRate; // 생성 주기
	private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간

	public float spawnRateMin = 0.5f; // 최소 생성 주기
	public float spawnRateMax = 3f; // 최대 생성 주기


    void Start()
    {
		// 최근 생성 이후의 누적 시간을 0으로 초기화
		timeAfterSpawn = 0f;
		// 생성 간격 랜덤
		spawnRate = Random.Range(spawnRateMin, spawnRateMax);

		Target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    void Update()
    {
		// timeAfterSpawn 갱신
		timeAfterSpawn += Time.deltaTime;

		// 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
		if (timeAfterSpawn >= spawnRate) 
		{
			// 누적된 시간을 리셋
			timeAfterSpawn = 0f;
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			bullet.transform.LookAt(Target);
			// 다음번 생성 간격 랜덤
			spawnRate = Random.Range(spawnRateMin, spawnRateMax);
		}

	}
}
