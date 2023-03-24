using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjects : MonoBehaviour
{
	protected GameObject _playerObj = default;
	protected string _popupTxt = string.Empty;
	protected bool _isActive = true;
	protected bool _disposable = false;
	public virtual void Interaction()
	{
		/* Do something */
	}




}
