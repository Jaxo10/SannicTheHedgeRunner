using UnityEngine;
using System.Collections;

public class Create_Setting : MonoBehaviour {
	
	//public int difficulty;
	public bool toggleMusic;
	public string playerName;

	void Start (){
		//difficulty = PlayerPrefs.GetInt ("dif");
        if (PlayerPrefs.HasKey("music"))
        {
            toggleMusic = PlayerPrefs.GetInt("music") == 1;
        }
        else toggleMusic = true;
		playerName = PlayerPrefs.GetString ("name");
	}

	void OnGUI (){
		

			GUI.skin = MyGUIManager.GetSkin();
			//float diff;

			int height = Screen.height;
			int width = Screen.width;
			GUILayout.BeginArea(new Rect (width / 6 , height / 4, (width*2) / 3, 400));
			
			GUILayout.Box ("Hedge Runner");
			
			GUILayout.BeginVertical();

			/*GUILayout.Label("Difficulty: " + difficulty.ToString());
			diff = GUILayout.HorizontalSlider(difficulty, 0.00f, 10.00f);
			difficulty = (int) diff; */

			GUILayout.Label("Player Name:");
			playerName = GUILayout.TextField(playerName, 25);

			toggleMusic = GUILayout.Toggle(toggleMusic, "   Music");

			if (GUILayout.Button ("Back")) {
				
				gameObject.AddComponent<Create_Menue>();
				Destroy(this);

				
				//PlayerPrefs.SetInt("dif", difficulty);
				if(!(playerName.Equals(""))) PlayerPrefs.SetString("name", playerName);
				if(toggleMusic) PlayerPrefs.SetInt("music", 1);
				else PlayerPrefs.SetInt("music", 0);

				PlayerPrefs.Save();
			}


			GUILayout.EndVertical();
			GUILayout.EndArea();

	}
}
