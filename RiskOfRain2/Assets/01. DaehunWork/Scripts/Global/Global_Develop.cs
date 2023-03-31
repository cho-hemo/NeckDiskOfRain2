using System.Collections.Generic;
using System.Linq;

namespace RiskOfRain2
{
	public static partial class Global
	{
		/// <summary>
		/// Collection이 유효한지 확인하는 함수 
		/// </summary>
		/// <param name="colleciton">Collection</param>
		/// <typeparam name="T">Generic</typeparam>
		/// <returns>
		/// 유효 : True, 유효하지 않으면 : False
		/// </returns>
		public static bool ValidCollection<T>(this IEnumerable<T> colleciton)
		{
			if (colleciton == null || !colleciton.Any())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 해당 콜렉션이 유효한지 value 요소가 존재하는지 확인하는 함수
		/// </summary>
		/// <param name="collection">Collection</param>
		/// <param name="value">값</param>
		/// <typeparam name="T">Generic</typeparam>
		/// <returns>
		/// 유효 : True, 유효하지 않으면 : False
		/// </returns>
		public static bool ValidCollectionElement<T>(this IEnumerable<T> collection, T value)
		{
			if (collection == null || !collection.Any())
			{
				return false;
			}

			return collection.Contains(value);
		}

		#region Print log func
		public static void Log(object message)
		{
#if DEBUG_MODE
			// Debug.Log(message);      2023-03-21 / HyungJun / 디버그가 너무 찍혀서 잠시 주석
#endif
		}
		#endregion
	}
}
