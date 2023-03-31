using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
	public Transform fireballSpawnPoint;
	public GameObject fireballPrefab;
	public float fireballSpeed = 10;

	public bool isCharge = default;
	public float breathCharge = default;

	void BreathCharge()
	{
		breathCharge += Time.deltaTime;
	}

	void Update()
	{
		if (isCharge) 
		{
			var fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
			fireball.GetComponent<Rigidbody>().velocity = fireballSpawnPoint.forward * fireballSpeed;
		}
	}

}

