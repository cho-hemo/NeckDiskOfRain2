using RiskOfRain2;
using RiskOfRain2.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	private static int s_currentMonsterCount;
    private const float SPAWN_CHECK_TIME = 10f;
    private const int MAX_MONSTER_COUNT = 20;

	private GameObject _spawnSpots;
    private GameObject _player;

	public static bool IsSpawnable()
	{
		bool isSpawnable = (s_currentMonsterCount < MAX_MONSTER_COUNT) ? true : false;
		return isSpawnable;
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
    }

    private void Start()
    {
		_spawnSpots = Global.FindRootObject(Functions.ROOT_SPAWN_SPOTS);
        _player = GameManager.Instance.Player.gameObject;
        SpawnMonster();
        StartCoroutine(SpawnMonsterLoop());
    }
}