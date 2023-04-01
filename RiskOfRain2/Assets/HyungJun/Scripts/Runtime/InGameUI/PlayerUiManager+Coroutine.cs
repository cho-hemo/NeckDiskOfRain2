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


	// PlayerBase a = GameManager.Instance.Player;
	// float av = a.Skills[PlayerDefine.PLAYER_SUB_SKILL_INDEX].SkillCooltime;


	/// <summary>
	/// 스킬을 눌렀을때 UI에 반영되는 함수
	/// </summary>
	/// <param name="num"></param>
	/// <param name="coolTime_"></param>
	/// <returns></returns>
	private IEnumerator SkillActive(int num, float coolTime_)
	{
		if (!_playerInfo.Skills[num].IsSkillCoolTime)
		{
			// Debug.Log("[PlayerUiManager] SkillActive : 스킬이 쿨타임 중입니다.");
			yield break;
		}
		// Debug.Log("[PlayerUiManager] SkillActive : 확인");
		// 스킬 비 활성화
		// 스킬 스택 오브젝트
		GameObject skillCount_ = _skillList[num].FindChildObj("SkillCostTxt");
		GameObject icon_ = _skillList[num].FindChildObj("Icon");
		GameObject coolDownObj_ = _skillList[num].FindChildObj("CoolDown");
		GameObject outLineBox_ = _skillList[num].FindChildObj("OutLineBox");

		coolDownObj_.SetActive(true);
		outLineBox_.SetActive(false);
		icon_.SetImageColor(0.5f, 0.5f, 0.5f);

		// 스킬 스택이 1이 아닌경우 표시해주고 1인 경우 꺼버린다.
		if (_playerInfo.Skills[num].SkillMaxStack != 1)
		{
			// Debug.Log("")
			skillCount_.SetTmpText($"{_playerInfo.Skills[num].SkillStack}");
		}
		else if (_playerInfo.Skills[num].SkillMaxStack == 1)
		{
			skillCount_.SetActive(false);
		}

		// 스킬 쿨타임 로직
		for (float i = 0.001f; i < coolTime_; i += 0.1f)
		{
			coolDownObj_.SetTmpText(((int)(coolTime_ - i + 1)).ToString());
			yield return new WaitForSeconds(0.1f);
		}

		// 스킬 재 활성화
		coolDownObj_.SetActive(false);
		outLineBox_.SetActive(true);
		icon_.SetImageColor(1f, 1f, 1f);
		if (_playerInfo.Skills[num].SkillMaxStack != 1)
		{
			skillCount_.SetTmpText($"{_playerInfo.Skills[num].SkillStack}");
		}
	}







}