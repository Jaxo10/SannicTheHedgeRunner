using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

	//public GUISkin MenueSkin;
	//public bool showOver;
	//public Create_Menue CM;
	//public Name Nam;
	//public string playerName;
    HS_Object myHS;
    string uid;
    int score;

	
	public void GameOver (int HighScore){
        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager) gameObject.GetComponent(typeof(HS_Manager));

        manager.seths(PlayerPrefs.GetString("name"), HighScore, PlayerPrefs.GetString("uniqueUID"));
        score = HighScore;
	}


	void Start()
	{
		if (!PlayerPrefs.HasKey("name")) {
				//showOver = false;
				//Nam.showName = true;
			
				gameObject.AddComponent<Name> ();
				Destroy (this);
		}

        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager)gameObject.GetComponent(typeof(HS_Manager));

        myHS = manager.geths();
        uid = PlayerPrefs.GetString("uniqueUID");
	}

	void OnGUI (){
			
			GUI.skin = MyGUIManager.GetSkin();
			
			int height = Screen.height;
			int width = Screen.width;
			GUILayout.BeginArea(new Rect (width / 6 , height / 4, (width*2) / 3, 400));
			
			GUILayout.Box ("Game Over");
			GUILayout.BeginVertical();

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label("You've reached " + score.ToString() + " Points!");

            int rank = 0;

            foreach (Score element in myHS.Scores)
            {
                rank++;

                if (element.uid == uid)
                {
                    GUILayout.BeginHorizontal();

                    GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                    GUILayout.Label(rank.ToString()+". "+element.name);

                    GUI.skin.label.alignment = TextAnchor.MiddleRight;
                    GUILayout.Label(element.score);

                    GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                    GUILayout.EndHorizontal();
                    break;
                }
            }


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

	}
}
