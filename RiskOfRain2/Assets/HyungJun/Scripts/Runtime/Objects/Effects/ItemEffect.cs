using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
	public GameObject ItemPrefab = default;

	private void Awake()
	{
		gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		// StartCoroutine(SpawnItem());
		// Debug.Log("짜잔");

		GetComponent<Rigidbody>().velocity += new Vector3(Random.Range(-2.0f, 2.0f), 5f, Random.Range(-2.0f, 2.0f));
	}

	// private IEnumerator SpawnItem(Rigidbody rigid_)
	// {
	// 	yield return null;


	// 	// gameObject.AddLocalPos()

	// 	// while (true)
	// 	// {
	// 	// 	transform.
	// 	// }
	// }
	private void OnTriggerEnter(Collider other)
	{
		// Debug.Log(other)
		if (other.tag == "Ground")
		{
			GameObject itemObj_ = transform.parent.GetChild(1).gameObject;
			itemObj_.transform.localPosition = transform.localPosition;
			itemObj_.SetActive(true);

			gameObject.SetActive(false);
		}
	}
}
