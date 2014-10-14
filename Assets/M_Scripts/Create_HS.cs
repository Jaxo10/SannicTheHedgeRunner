using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Create_HS : MonoBehaviour {

	HS_Object myHS;
    Vector2 ScrollPosition;


	void Start () {
	
        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager) gameObject.GetComponent(typeof(HS_Manager));

        myHS = manager.geths();
        ScrollPosition = new Vector2();
	}

    void OnGUI()
    {

        GUI.skin = MyGUIManager.GetSkin();


        int height = Screen.height;
        int width = Screen.width;
        GUILayout.BeginArea(new Rect(width / 6, height / 6, (width * 2) / 3, 400));

        GUILayout.Box("High Scores");

        ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        foreach (Score element in myHS.Scores)
        {
            GUILayout.BeginHorizontal();

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(element.name);

            GUI.skin.label.alignment = TextAnchor.MiddleRight;
            GUILayout.Label(element.score);

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Back"))
        {


            gameObject.AddComponent<Create_Menue>();
            Destroy(this);
        }

        

        GUILayout.EndArea();

    }
}
