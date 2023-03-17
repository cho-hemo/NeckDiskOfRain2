using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerUiManager : MonoBehaviour
{
    private IEnumerator LevelBarController()
    {
        int times_ = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            ++times_;
            if (times_ == 37)
            {
                _levelBarObj.transform.localPosition += Vector3.left;
                times_ = 0;
                if (_levelBarMoveValue % 40 == 0)
                {
                    ++MonsterLevel;
                }
                ++_levelBarMoveValue;
            }
        }
    }



    private IEnumerator SkillActive(int num, float coolTime_)
    {
        if (!_isSkillActivation[num]) { yield break; }

        // 스킬 비 활성화
        _isSkillActivation[num] = false;
        GameObject icon_ = _skillList[num].FindChildObj("Icon");
        GameObject coolDownObj_ = _skillList[num].FindChildObj("CoolDown");
        GameObject outLineBox_ = _skillList[num].FindChildObj("OutLineBox");
        coolDownObj_.SetActive(true);
        outLineBox_.SetActive(false);
        icon_.ImageColorControll(0.5f, 0.5f, 0.5f);

        // 스킬 쿨타임 로직
        int currentCoolTime_ = (int)coolTime_;
        for (float i = 0.001f; i < coolTime_; i += 0.1f)
        {
            coolDownObj_.SetTmpText(((int)(coolTime_ - i + 1)).ToString());
            yield return new WaitForSeconds(0.1f);
        }

        // 스킬 재 활성화
        _isSkillActivation[num] = true;
        coolDownObj_.SetActive(false);
        outLineBox_.SetActive(true);
        icon_.ImageColorControll(1f, 1f, 1f);
    }







}