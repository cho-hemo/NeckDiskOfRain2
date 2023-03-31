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
			GameObject item_ = Instantiate(ItemPrefab, transform.parent);
			item_.transform.localPosition = transform.localPosition;
			Debug.Log(item_.activeSelf);
			item_.SetActive(true);
			Debug.Log(item_.activeSelf);

			gameObject.SetActive(false);
		}
	}
}
