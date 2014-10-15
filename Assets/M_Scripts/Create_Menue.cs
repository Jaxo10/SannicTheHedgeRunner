﻿using UnityEngine;
using System.Collections;

public class Create_Menue : MonoBehaviour {

	//public GUISkin MenueSkin;
	//public bool showMenue;
	//public Create_Setting CS;
	
	void Start (){
		
		if(!PlayerPrefs.HasKey("uniqueUID")){
			
			//todo: hash a UID
			string hash = "i834numdw38";
			PlayerPrefs.SetString("uniqueUID", hash);
		}
		
	}

	void OnGUI (){

		//if (showMenue) {

			GUI.skin = MyGUIManager.GetSkin();
					
			int height = Screen.height;
			int width = Screen.width;
			GUILayout.BeginArea(new Rect (width / 6 , height / 4, (width*2) / 3, 400));

			GUILayout.Box ("Hedge Runner");
			GUILayout.BeginVertical();

			if (Application.loadedLevel == 1){
					
				if (GUILayout.Button ("Continue")) {
					Destroy(this);	
					Time.timeScale = 1;
				}
			}
			else {
				if (GUILayout.Button ("Play")) {
					Application.LoadLevel (1);		
				}

			}

			if (GUILayout.Button ("Settings")) {
				
				//showMenue = false;
				//CS.showSettings = true;
				
				gameObject.AddComponent<Create_Setting>();
				Destroy(this);
			}
            if (GUILayout.Button("Highscores"))
            {

                //showMenue = false;
                //CS.showSettings = true;

                gameObject.AddComponent<Create_HS>();
                Destroy(this);
            }

			GUILayout.EndVertical();
			GUILayout.EndArea();
		//}
	}
}