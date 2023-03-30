namespace RiskOfRain2.Player
{
	public static class PlayerDefine
	{
		public static string PLAYER_IS_MAIN_SKILL = "IsMainSkill";
		public static string PLAYER_IS_SUB_SKILL = "IsSubSkill";
		public static string PLAYER_IS_UTILITY_SKILL = "IsUtilitySkill";
		public static string PLAYER_IS_SPECIAL_SKILL = "IsSpecialSkill";
		public static string PLAYER_MAIN_SKILL = "MainSkill";
		public static string PLAYER_SUB_SKILL = "SubSkill";
		public static string PLAYER_UTILITY_SKILL = "UtilitySkill";
		public static string PLAYER_SPECIAL_SKILL = "SpecialSkill";
		public static string ATTACK_SPEED = "AttackSpeed";
		public static string MOVE_SPEED = "MoveSpeed";
		public static int PLAYER_BASE_LAYER = 0;
		public static int PLAYER_AIM_HORIZONTAL_LAYER = 1;
		public static int PLAYER_AIM_VERTICAL_LAYER = 2;
		public static int PLAYER_ATTACK_LAYER = 3;

		public static int PLAYER_MAIN_SKILL_INDEX = 0;
		public static int PLAYER_SUB_SKILL_INDEX = 1;
		public static int PLAYER_UTILITY_SKILL_INDEX = 2;
		public static int PLAYER_SPECIAL_SKILL_INDEX = 3;
	}
	public enum PlayerType
	{
		NONE, COMMANDO,
	}

	public enum PlayerStat
	{
		NONE, MAX_HP, CURRENT_HP, ATTACK_DAMAGE, ATTACK_SPEED, DEFENSE, WALK_SPEED, SPRINT_SPEED, JUMP_COUNT, JUMP_HEIGHT,
	}

	public enum direction
	{
		NONE, LEFT, RIGHT, FORWARD, BACK
	}
}