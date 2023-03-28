using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace RiskOfRain2
{
	public static partial class Global
	{
		public static void AddOnSceneLoaded(UnityAction<Scene, LoadSceneMode> sceneLoaded)
		{
			SceneManager.sceneLoaded += sceneLoaded;
		}

		public static Scene GetActiveScene()
		{
			Scene resultScene_ = SceneManager.GetActiveScene();
			return resultScene_;
		}

		public static Scene GetSceneByName(string sceneName)
		{
			Scene resultScene_ = SceneManager.GetSceneByName(sceneName);
			return resultScene_;
		}

		public static Scene GetSceneByIndex(int index)
		{
			Scene resultScene_ = SceneManager.GetSceneAt(index);
			return resultScene_;
		}
	}
}