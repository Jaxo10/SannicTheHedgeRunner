using UnityEngine;
using System.Collections;

public class MyGUIManager : MonoBehaviour {

		public GUISkin guiSkin;
		
		private static MyGUIManager instance;
		
		void Awake()
		{
			instance = this;
		}
		
		public static GUISkin GetSkin()
		{
			return instance.guiSkin;
		}

}
