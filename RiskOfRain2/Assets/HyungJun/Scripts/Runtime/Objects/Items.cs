using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
	// Start is called before the first frame update
	protected virtual void Start()
	{
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		StartCoroutine(SpinObject());
	}


	protected IEnumerator SpinObject()
	{
		float rotateValue_ = 0f;
		float repetitionValue_ = 0f;
		int reverseValue_ = 1;
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			rotateValue_ += 1f;
			repetitionValue_ += 0.1f * reverseValue_;


			if (3 < repetitionValue_)
			{
				reverseValue_ = -1;
			}
			else if (-3 >= repetitionValue_)
			{
				reverseValue_ = +1;
			}


			transform.rotation = Quaternion.Euler(-90, 0f, rotateValue_);
			transform.position += Vector3.up * repetitionValue_ * 0.001f;
		}
	}       // SpineObject()

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}

}
