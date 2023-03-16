using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Player_Commando : Player
{
    public Transform IKARMTEST_L;
    public Transform IKARMTEST_R;
    public new void Start()
    {
        base.Start();
        PlayerType = PlayerType.COMMANDO;

        //StartCoroutine(Shoot());
    }
    // public IEnumerator Shoot()
    // {
    //     while (true)
    //     {
    //         // if (!Input.shoot)
    //         // {
    //         //     yield return new WaitForSeconds(1 / AttackDelay);
    //         //     continue;
    //         // }
    //         GameObject tempObject = ObjectPoolManager.Instance.ObjectPoolPop(ObjectPoolManager.BULLET);
    //         tempObject.transform.localPosition = FocusPoint.position;
    //         tempObject.SetActive(true);
    //         yield return new WaitForSeconds(1 / AttackDelay);
    //     }
    // }

    public override void PassiveSkill()
    {

    }

    public override void MainSkill(bool isPressed_)
    {
        PlayerAnimator.SetBool("IsMainSkill", isPressed_);
    }

    public override void SubSkill(bool isPressed_)
    {
        PlayerAnimator.SetTrigger("SubSkill");
    }

    public override void UtilitySkill(bool isPressed_)
    {
        if (isPressed_)
        {
            StateMachine.SetState(new Player_Commando_RollState(this));
        }
    }

    public override void SpecialSkill(bool isPressed_)
    {

    }
}