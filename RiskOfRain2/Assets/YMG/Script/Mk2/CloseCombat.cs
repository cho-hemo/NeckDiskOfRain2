using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	public BoxCollider closeCombat;

	private void Start()
	{
		closeCombat.enabled = false;

	}

	private void On()
	{
		Debug.Log("On");
		closeCombat.enabled = true;
	}

	private void Off()
	{
		Debug.Log("Off");
		closeCombat.enabled = false;
	}
}
