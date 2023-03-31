using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FuncTest2 : MonoBehaviour
{
	public float speed = 8f;
	private Rigidbody bulletRigidbody;

	void Start()
	{
		bulletRigidbody = GetComponent<Rigidbody>();
		bulletRigidbody.velocity = transform.forward * speed;

		Destroy(gameObject, 3f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log("Hit");
		}
	}
}
