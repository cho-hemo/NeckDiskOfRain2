using RiskOfRain2.Item;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace RiskOfRain2.Manager
{
	public class InventoryManager : SingletonBase<InventoryManager>
	{
		public List<ItemBase> items = new List<ItemBase>();

		private new void Awake()
		{
			base.Awake();
		}

		public void ItemAdd(ItemBase item)
		{
			item.ItemGet();
			ItemBase tempItem = items.Find(x => x.itemName == item.itemName);
			if (tempItem == null)
			{
				items.Add(item);
			}
			else
			{
				tempItem.itemNumber += 1;
			}
		}
	}
}