using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

    HS_Object myHS;
    string uid;
    int score;

	
	public void GameOver (int HighScore){
        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager) gameObject.GetComponent(typeof(HS_Manager));

        myHS = manager.seths(PlayerPrefs.GetString("name"), HighScore, PlayerPrefs.GetString("uniqueUID"));
        score = HighScore;

	}


	void Start()
	{
        //Ensure that a Name is set
		if (!PlayerPrefs.HasKey("name") || (PlayerPrefs.GetString("name").Equals(""))) {
			
				gameObject.AddComponent<Name> ();
				Destroy (this);
		}

        //Get the score and call GameOver to save it
        GameObject GM = GameObject.Find("GameManager");
        ScoreManager SM = (ScoreManager)GM.GetComponent(typeof(ScoreManager));
        float score = SM.score;

        GameOver((int)score);

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

            if (myHS != null)
            {
                foreach (Score element in myHS.Scores)
                {
                    rank++;

                    if (element.uid == uid)
                    {
                        GUILayout.BeginHorizontal();

                        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                        GUILayout.Label(rank.ToString() + ". " + element.name);

                        GUI.skin.label.alignment = TextAnchor.MiddleRight;
                        GUILayout.Label(element.score);

                        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                        GUILayout.EndHorizontal();
                        break;
                    }
                }
            }
            else GUILayout.Label("Failed to load scores.");

			if(GUILayout.Button ("Restart")){
                myHS = null;
				Time.timeScale = 1;
				Application.LoadLevel (1);
			}

            if (Input.GetButtonDown("Jump")) {
                myHS = null;
                Time.timeScale = 1;
                Application.LoadLevel(1);
            }

			if (GUILayout.Button ("Back")) {

                myHS = null;
				Time.timeScale = 1;
				Application.LoadLevel(0);

			}

			GUILayout.EndVertical();
			GUILayout.EndArea();

	}
}
