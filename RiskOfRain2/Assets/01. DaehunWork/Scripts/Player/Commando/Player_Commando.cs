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

    public override void MainSkill(bool isPressed_)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        SetBool(Global.PLAYER_IS_MAIN_SKILL, isPressed_);
    }

    public override void SubSkill(bool isPressed_)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        SetTrigger(Global.PLAYER_SUB_SKILL);
    }

    public override void UtilitySkill(bool isPressed_)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        if (isPressed_)
        {
            SetState(new Player_Commando_RollState(this));
        }
    }

    public override void SpecialSkill(bool isPressed_)
    {
        switch (StateMachine.GetState())
        {
            case Player_Commando_RollState:
                return;
        }
        if (isPressed_)
        {
            SetBool(Global.PLAYER_IS_SPECIAL_SKILL, isPressed_);
        }
    }
}