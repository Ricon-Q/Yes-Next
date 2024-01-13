using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ItemData", "stackSize")]
	public class ES3UserType_InventorySlot : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_InventorySlot() : base(typeof(InventorySlot)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (InventorySlot)obj;
			
			writer.WritePropertyByRef("ItemData", instance.ItemData);
			writer.WritePrivateField("stackSize", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (InventorySlot)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ItemData":
						instance.ItemData = reader.Read<InventoryItemData>();
						break;
					case "stackSize":
					instance = (InventorySlot)reader.SetPrivateField("stackSize", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new InventorySlot();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_InventorySlotArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InventorySlotArray() : base(typeof(InventorySlot[]), ES3UserType_InventorySlot.Instance)
		{
			Instance = this;
		}
	}
}