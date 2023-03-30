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
			_hpBarList.Add(hpBar_);
		}
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < _objectList.Count; i++)
		{
			_hpBarList[i].transform.position = _cam.WorldToScreenPoint(_objectList[i].position + new Vector3(0, 1.15f, 0f));
		}
	}
}
