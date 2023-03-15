using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Player_Commando : Player
{
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

    public override void MainSkill()
    {

    }

    public override void SubSkill()
    {

    }

    public override void UtilitySkill()
    {
        StateMachine.SetState(new Player_Commando_RollState(this));
    }

    public override void SpecialSkill()
    {

    }

}