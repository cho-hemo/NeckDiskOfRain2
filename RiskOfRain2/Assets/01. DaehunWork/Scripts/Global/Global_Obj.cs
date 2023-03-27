using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RiskOfRain2
{
	public static partial class Global
	{
		public static GameObject FindRootObject(string objName)
		{
			GameObject[] rootObjs_ = Global.GetActiveScene().GetRootGameObjects();
			GameObject result_ = default;
			foreach (var obj in rootObjs_)
			{
				if (obj.name.Equals(objName))
				{
					result_ = obj;
				}
			}
			if (result_.Equals(null) || result_.Equals(default))
			{
				return null;
			}
			else
			{
				return result_;
			}
		}

		public static GameObject FindChildObject(this GameObject obj, string objName)
		{
			Queue<GameObject> objs_ = new Queue<GameObject>();
			objs_.Enqueue(obj);
			while (0 < objs_.Count)
			{
				GameObject tempObj = objs_.Dequeue();
				if (tempObj.name.Equals(objName))
				{
					return tempObj;
				}
				foreach (var iterator in tempObj.GetChildsObject())
				{
					objs_.Enqueue(iterator);
				}
			}

			return null;
		}

		public static List<GameObject> GetChildsObject(this GameObject obj)
		{
			List<GameObject> resultObjs_ = new List<GameObject>();
			int objCount = obj.transform.childCount;
			for (int i = 0; i < objCount; i++)
			{
				resultObjs_.Add(obj.transform.GetChild(i).gameObject);
			}

			return resultObjs_;
		}
	}
}