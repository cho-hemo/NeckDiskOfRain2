using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Player_Commando : Player
{
    protected new void Start()
    {
        base.Start();
        PlayerType = PlayerType.COMMANDO;

    }

    public override void PassiveSkill()
    {
    }

    public override void MainSkill(bool isPressed)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        SetBool(Global.PLAYER_IS_MAIN_SKILL, isPressed);
        SetFloat(Global.ATTACK_SPEED, AttackSpeed);
    }

    public override void SubSkill(bool isPressed)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        if (isPressed)
        {
            SetTrigger(Global.PLAYER_SUB_SKILL);
        }
    }

    public override void UtilitySkill(bool isPressed)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        if (isPressed)
        {
            SetState(new Player_Commando_RollState(this));
        }
    }

    public override void SpecialSkill(bool isPressed)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        if (isPressed)
        {
            StartCoroutine(SpecialSkillCoroutine(isPressed));
        }
    }

    public void MainSkillShot()
    {
        Vector3 pos_ = default;
        Quaternion rotation_ = default;
        AnimatorStateInfo currentStateInfo_ = PlayerAnimator.GetCurrentAnimatorStateInfo(Global.PLAYER_ATTACK_LAYER);
        if (currentStateInfo_.IsName("FirePistol_Left"))
        {
            pos_ = FocusPoint[0].position;
            rotation_ = transform.rotation;
            BulletShot(pos_, rotation_, AttackDamage);
        }
        else if (currentStateInfo_.IsName("FirePistol_Right"))
        {
            pos_ = FocusPoint[1].position;
            rotation_ = transform.rotation;
            BulletShot(pos_, rotation_, AttackDamage);
        }
    }

    public void BulletShot(Vector3 pos, Quaternion rotation, float damage)
    {
        GameObject bullet_ = ObjectPoolManager.Instance.ObjectPoolPop("Bullet");
        bullet_.transform.localPosition = pos;
        bullet_.transform.rotation = rotation;
        bullet_.SetActive(true);
    }


    IEnumerator SpecialSkillCoroutine(bool isPressed)
    {
        int loopCount_ = Mathf.RoundToInt(AttackSpeed * 6);
        for (int i = 0; i < loopCount_; i++)
        {
            SetTrigger(Global.PLAYER_SPECIAL_SKILL);
            yield return new WaitForSeconds(1 / loopCount_);

        }
    }
}