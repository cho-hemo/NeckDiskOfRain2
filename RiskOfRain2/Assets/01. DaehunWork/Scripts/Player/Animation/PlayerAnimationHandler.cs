using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField]
    private Player _player;
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
        _player.PlayerAnimator.SetBool(Global.PLAYER_IS_UTILITY_SKILL, false);
    }

    public void AnimationAction()
    {

    }


}