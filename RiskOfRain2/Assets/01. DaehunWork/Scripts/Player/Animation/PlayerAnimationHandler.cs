using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using RiskOfRain2;
using RiskOfRain2.Player;

public class PlayerAnimationHandler : MonoBehaviour
{
	[SerializeField]
	private RiskOfRain2.Player.PlayerBase _player;
	private void Start()
	{
		TryGetComponent(out _player);
	}

	public void AnimationStart()
	{

	}

	public void AnimationComplete()
	{
		//_player.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
		//Global.Log($"AnimationComplete : {_player.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Roll")}");
		_player.PlayerAnimator.SetBool(PlayerDefine.PLAYER_IS_UTILITY_SKILL, false);
	}

	public void AnimationAction()
	{

	}


}