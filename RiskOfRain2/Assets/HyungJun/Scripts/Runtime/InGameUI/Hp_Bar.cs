using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

public class Hp_Bar : MonoBehaviour
{
	private Camera _cam = default;

	// 몬스터의 ID에 맞는 HP바 딕셔너리
	private Dictionary<int, GameObject> _hpBarInstanceIdDic = new Dictionary<int, GameObject>();
	// private Transform HP_BarTp = default;

	void Start()
	{
		_cam = Camera.main;

		// GameObject[] objects_ = GameObject.FindGameObjectsWithTag("Enemy");
		// for (int i = 0; i < objects_.Length; i++)
		// {
		// 	_objectList.Add(objects_[i].transform);
		// 	GameObject hpBar_ = Instantiate(_goPrefab, objects_[i].transform.position, Quaternion.identity, transform);
		// 	hpBar_.name = objects_[i].name;
		// 	_hpBarList.Add(hpBar_);
		// }
	}

	/// <summary>
	/// 몬스터가 스폰될 때 Hp바를 딕셔너리에 추가하는 로직
	/// </summary>
	/// <param name="id_">해당 몬스터의 ID 값</param>
	public void MakeHpBarFromInstanceId(int id_)
	{
		GameObject hpBar_ = ObjectPoolManager.Instance.ObjectPoolPop("Hp_Bar");
		// 풀에서 꺼내서 부모오브젝트를 캔버스에 넣는 함수
		hpBar_.transform.SetParent(transform);
		hpBar_.SetActive(true);
		_hpBarInstanceIdDic.Add(id_, hpBar_);
	}

	/// <summary>
	/// 몬스터가 자신의 hp바를 계속해서 출력하는 함수 (Update 용 함수)
	/// </summary>
	/// <param name="id_">몬스터의 Intance ID 값</param>
	/// <param name="objTp_">해당 몬스터의 트랜스폼</param>
	public void UpdateHpBarPosition(int id_, Transform objTp_)
	{
		_hpBarInstanceIdDic[id_].transform.position = _cam.WorldToScreenPoint(objTp_.position);
	}

	/// <summary>
	/// 몬스터가 죽을때 hp바를 풀에 넣고 꺼주는 함수
	/// </summary>
	/// <param name="id_">몬스터의 Instance ID 값</param>
	public void deleteHpBar(int id_)
	{
		GameObject hpBar_ = _hpBarInstanceIdDic[id_];
		_hpBarInstanceIdDic.Remove(id_);
		// 딕셔너리에서 꺼내서 부모 오브젝트를 바꾸는 함수
		hpBar_.transform.SetParent(transform);
		hpBar_.SetActive(false);
		ObjectPoolManager.Instance.ObjectPoolPush(hpBar_);
	}

	/// <summary>
	/// 몬스터의 체력 표시 바의 상태를 바꾸는 함수
	/// </summary>
	/// <param name="monsterName_">몬스터 오브젝트의 이름</param>
	/// <param name="monsterMaxHp_">몬스터의 최대 체력</param>
	/// <param name="monsterCurrentHp_">몬스터의 현재 체력</param>
	public void MonsterHpGaugeDown(int id_, int monsterCurrentHp_, int monsterMaxHp_)
	{
		GameObject hpBar_ = _hpBarInstanceIdDic[id_];

		hpBar_.FilledImageControll((float)monsterCurrentHp_ / (float)monsterMaxHp_);
	}


	// // Update is called once per frame
	// void Update()
	// {
	// 	for (int i = 0; i < _objectList.Count; i++)
	// 	{
	// 		_hpBarList[i].transform.position = _cam.WorldToScreenPoint(_objectList[i].position + new Vector3(0, 3f, 0f));
	// 	}
	// }


	// 디버그 모드
	// public void Debug_Test()
	// {
	// 	MonsterHpGaugeDown("Cube", 40f, 10f);
	// }
}
