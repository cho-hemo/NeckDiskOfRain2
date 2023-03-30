using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_Bar : MonoBehaviour
{
	[SerializeField] private GameObject _goPrefab = default;

	private List<Transform> _objectList = new List<Transform>();
	private List<GameObject> _hpBarList = new List<GameObject>();

	private Camera _cam = default;


	void Start()
	{
		_cam = Camera.main;

		GameObject[] objects_ = GameObject.FindGameObjectsWithTag("Monster");
		for (int i = 0; i < objects_.Length; i++)
		{
			_objectList.Add(objects_[i].transform);
			GameObject hpBar_ = Instantiate(_goPrefab, objects_[i].transform.position, Quaternion.identity, transform);
			hpBar_.name = objects_[i].name;
			_hpBarList.Add(hpBar_);
		}
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < _objectList.Count; i++)
		{
			_hpBarList[i].transform.position = _cam.WorldToScreenPoint(_objectList[i].position + new Vector3(0, 1f, 0f));
		}
	}

	/// <summary>
	/// 몬스터의 체력 표시 바의 상태를 바꾸는 함수
	/// </summary>
	/// <param name="monsterName_">몬스터 오브젝트의 이름</param>
	/// <param name="monsterMaxHp_">몬스터의 최대 체력</param>
	/// <param name="monsterCurrentHp_">몬스터의 현재 체력</param>
	public void MonsterHpGaugeDown(string monsterName_, float monsterMaxHp_, float monsterCurrentHp_)
	{
		GameObject hpBar_ = gameObject.FindChildObj(monsterName_).FindChildObj("HpBar");
		Debug.Log(monsterName_);

		hpBar_.FilledImageControll(monsterCurrentHp_ / monsterMaxHp_);
	}

	// 디버그 모드
	public void Debug_Test()
	{
		MonsterHpGaugeDown("Cube", 40f, 10f);
	}
}
