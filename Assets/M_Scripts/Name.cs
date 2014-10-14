using UnityEngine;
using System.Collections;

public class Name : MonoBehaviour {

	//public GUISkin MenueSkin;
	//public bool showName;
	//public Game_Over GO;
	public string playerName;

	void Start()
	{
		playerName = "";
	}

    void OnGUI()
    {
		//if (showName) {

				GUI.skin = MyGUIManager.GetSkin();
				
				int height = Screen.height;
				int width = Screen.width;
				GUILayout.BeginArea(new Rect (width / 6 , height / 4, (width*2) / 3, 400));
				
				
				GUILayout.BeginVertical();
				
				GUILayout.Box("Enter your name");
				playerName = GUILayout.TextField(playerName, 25);
				
				if (GUILayout.Button ("Save")) {

					PlayerPrefs.SetString("name", playerName);
					PlayerPrefs.Save ();
					
					//showName = false;
					//GO.showOver = true;

					gameObject.AddComponent<Game_Over>();
					Destroy(this);
					
				}
				
				GUILayout.EndVertical();
				GUILayout.EndArea();

		//}
    }
}
