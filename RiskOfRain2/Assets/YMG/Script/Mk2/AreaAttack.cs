using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
	public SphereCollider areaAttack;

    void Start()
	{
		areaAttack.enabled = false;
	}

	private void On()
	{
		Debug.Log("On");
		areaAttack.enabled = true;
	}

	private void Off()
	{
		Debug.Log("Off");
		areaAttack.enabled = false;
	}

}
