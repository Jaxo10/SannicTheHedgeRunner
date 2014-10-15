using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

	//public GUISkin MenueSkin;
	//public bool showOver;
	public int HighS;
	//public Create_Menue CM;
	//public Name Nam;
	//public string playerName;

	
	public void GameOver (int HighScore){
        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager) gameObject.GetComponent(typeof(HS_Manager));

        manager.seths(PlayerPrefs.GetString("name"), HighScore, PlayerPrefs.GetString("uniqueID"));
	}


	void Start()
	{
		if (!PlayerPrefs.HasKey("name")) {
				//showOver = false;
				//Nam.showName = true;
			
				gameObject.AddComponent<Name> ();
				Destroy (this);
		}

	}

	void OnGUI (){
		
		//if (showOver) {

			
			GUI.skin = MyGUIManager.GetSkin();
			
			int height = Screen.height;
			int width = Screen.width;
			GUILayout.BeginArea(new Rect (width / 6 , height / 4, (width*2) / 3, 400));
			
			GUILayout.Box ("Game Over");
			GUILayout.BeginVertical();

			if(GUILayout.Button ("Restart")){
				Time.timeScale = 1;
				Application.LoadLevel (1);
			}
			
			if (GUILayout.Button ("Back")) {
				
				//showOver = false;
				//CM.showMenue = true;
				Time.timeScale = 1;
				Application.LoadLevel(0);

			}

			GUILayout.EndVertical();
			GUILayout.EndArea();
			
		//}
	}
}
