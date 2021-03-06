﻿using UnityEngine;
using System.Collections;
using System;

public class Create_Menue : MonoBehaviour {

	
	void Start (){

        if(!PlayerPrefs.HasKey("uniqueUID")){

            string hash = Guid.NewGuid().ToString("N");
			PlayerPrefs.SetString("uniqueUID", hash);
		}
		
	//Check if double
	Create_Menue[] mens = gameObject.GetComponents<Create_Menue>();
	if(mens.Length > 1) Destroy(this);
	
	}

	void OnGUI (){


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

				gameObject.AddComponent<Create_Setting>();
				Destroy(this);
			}
            if (GUILayout.Button("Highscores"))
            {

                gameObject.AddComponent<Create_HS>();
                Destroy(this);
            }

            if (GUILayout.Button("Exit"))
            {
                Application.Quit();
            }

			GUILayout.EndVertical();
			GUILayout.EndArea();

	}
}
