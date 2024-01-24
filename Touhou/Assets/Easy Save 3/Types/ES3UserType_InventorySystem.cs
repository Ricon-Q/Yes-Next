using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("savePath", "InventorySlots", "OnInventorySlotChanged")]
	public class ES3UserType_InventorySystem : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_InventorySystem() : base(typeof(InventorySystem)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (InventorySystem)obj;
			
			writer.WriteProperty("savePath", instance.savePath, ES3Type_string.Instance);
			writer.WriteProperty("InventorySlots", instance.InventorySlots, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<InventorySlot>)));
			writer.WriteProperty("OnInventorySlotChanged", instance.OnInventorySlotChanged, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.Events.UnityAction<InventorySlot>)));
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (InventorySystem)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "savePath":
						instance.savePath = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "InventorySlots":
						instance.InventorySlots = reader.Read<System.Collections.Generic.List<InventorySlot>>();
						break;
					case "OnInventorySlotChanged":
						instance.OnInventorySlotChanged = reader.Read<UnityEngine.Events.UnityAction<InventorySlot>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_InventorySystemArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InventorySystemArray() : base(typeof(InventorySystem[]), ES3UserType_InventorySystem.Instance)
		{
			Instance = this;
		}
	}
}