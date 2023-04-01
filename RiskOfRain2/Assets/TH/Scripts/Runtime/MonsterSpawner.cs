using RiskOfRain2;
using RiskOfRain2.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	public static int s_currentMonsterCount { get; private set; } = 0;
	public const int MAX_MONSTER_COUNT = 20;
	private const float SPAWN_CHECK_TIME = 10f;

	private GameObject _spawnSpots;
	private GameObject _player;

    private List<string> _monsterPrefabNames = new List<string>()
    {
        "BeetleMK2",
		"Lemurian",
		"Golem"
	};

	public static void AddMonsterCount()
	{
		++s_currentMonsterCount;
	}

	public static void ReduceMonsterCount()
	{
		--s_currentMonsterCount;
	}

	private IEnumerator SpawnMonsterLoop()
	{
		while (true)
		{
			yield return new WaitForSeconds(SPAWN_CHECK_TIME);

			SpawnMonster();
		}
	}

	private void SpawnMonster()
	{
		float minSqrDistance = float.MaxValue;
		Vector3 spawnPos = Vector3.zero;

		if (s_currentMonsterCount >= MAX_MONSTER_COUNT)
		{
			return;
		}

		for (int i = 0; i < _spawnSpots.transform.childCount; i++)
		{
			Vector3 childPos = _spawnSpots.transform.GetChild(i).position;

			float sqrDistance = Functions.GetSqrDistance(_player.transform.position, childPos);
			if (sqrDistance < minSqrDistance)
			{
				minSqrDistance = sqrDistance;
				spawnPos = childPos;
			}
		}

        int randomNum = Random.Range(0, _monsterPrefabNames.Count);
        GameObject monster = ObjectPoolManager.Instance.ObjectPoolPop(_monsterPrefabNames[randomNum]);
		monster.transform.position = spawnPos;
        monster.transform.rotation = Quaternion.Euler(_player.transform.position - monster.transform.position).normalized;
		monster.GetComponent<MonsterBase>().Initialize();
        monster.gameObject.SetActive(true);
		AddMonsterCount();
	}

	private void Start()
	{
		_spawnSpots = Global.FindRootObject(Functions.ROOT_SPAWN_SPOTS);
		_player = GameManager.Instance.Player.gameObject;
		SpawnMonster();
		StartCoroutine(SpawnMonsterLoop());
	}
}