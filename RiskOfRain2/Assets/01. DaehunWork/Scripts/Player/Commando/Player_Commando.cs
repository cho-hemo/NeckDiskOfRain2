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