using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiskOfRain2.Player
{
	public class PlayerController : MonoBehaviour
	{
		private PlayerBase _player = default;

		private void Start()
		{
			_player = GetComponent<PlayerBase>();
		}

		public void MoveInput(Vector2 value)
		{
			_player.Move(value);
		}

		public void LookInput(Vector2 value)
		{
			_player.Look(value);
		}

		public void JumpInput(bool isPressed)
		{
			_player.Jump(isPressed);
		}

		public void SprintInput()
		{
			_player.Sprint();
		}

		public void SprintInput(bool isPressed)
		{
			_player.Sprint();
		}

		public void MainSkillInput(bool isPressed)
		{
			_player.MainSkill(isPressed);
		}

		public void SubSkillInput(bool isPressed)
		{
			_player.SubSkill(isPressed);
		}

		public void UtilitySkillInput(bool isPressed)
		{
			_player.UtilitySkill(isPressed);
		}

		public void SpecialSkillInput(bool isPressed)
		{
			_player.SpecialSkill(isPressed);
		}

		public void UseEquipment(bool isPressed)
		{

		}

		public void Interaction(bool isPressed)
		{

		}

		public void Esc(bool isPressed)
		{

		}
	}
}
