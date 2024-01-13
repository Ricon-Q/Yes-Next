using System;
using UnityEngine;
using ES3Types;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ID", "ItemType", "DisplayName", "Description", "Icon", "MaxStackSize", "Shopable", "SellPrice", "BuyPrice")]
	public class ES3UserType_InventoryItemData : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_InventoryItemData() : base(typeof(InventoryItemData)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (InventoryItemData)obj;
			
			writer.WriteProperty("ID", instance.ID, ES3Type_int.Instance);
			writer.WriteProperty("ItemType", instance.ItemType, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ItemType)));
			writer.WriteProperty("DisplayName", instance.DisplayName, ES3Type_string.Instance);
			writer.WriteProperty("Description", instance.Description, ES3Type_string.Instance);
			writer.WritePropertyByRef("Icon", instance.Icon);
			writer.WriteProperty("MaxStackSize", instance.MaxStackSize, ES3Type_int.Instance);
			writer.WriteProperty("Shopable", instance.Shopable, ES3Type_bool.Instance);
			writer.WriteProperty("SellPrice", instance.SellPrice, ES3Type_long.Instance);
			writer.WriteProperty("BuyPrice", instance.BuyPrice, ES3Type_long.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (InventoryItemData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ID":
						instance.ID = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "ItemType":
						instance.ItemType = reader.Read<ItemType>();
						break;
					case "DisplayName":
						instance.DisplayName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "Description":
						instance.Description = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "Icon":
						instance.Icon = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "MaxStackSize":
						instance.MaxStackSize = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Shopable":
						instance.Shopable = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "SellPrice":
						instance.SellPrice = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "BuyPrice":
						instance.BuyPrice = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_InventoryItemDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InventoryItemDataArray() : base(typeof(InventoryItemData[]), ES3UserType_InventoryItemData.Instance)
		{
			Instance = this;
		}
	}
}