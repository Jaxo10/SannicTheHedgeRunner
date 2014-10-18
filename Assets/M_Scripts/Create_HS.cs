using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Create_HS : MonoBehaviour {

	HS_Object myHS;
    Vector2 ScrollPosition;
    string uid;


	void Start () {
	
        gameObject.AddComponent<HS_Manager>();
        HS_Manager manager = (HS_Manager) gameObject.GetComponent(typeof(HS_Manager));

        myHS = manager.geths();
        ScrollPosition = new Vector2();
        uid = PlayerPrefs.GetString("uniqueUID");
	}

    void OnGUI()
    {

        GUI.skin = MyGUIManager.GetSkin();


        int height = Screen.height;
        int width = Screen.width;
        GUILayout.BeginArea(new Rect(width / 6, height / 6, (width * 2) / 3, 400));

        GUILayout.Box("High Scores");

        ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
	    int HScounter = 0;
	
        foreach (Score element in myHS.Scores)
        {
              HScounter++;
          
              if(HScounter < 9 && element.uid == uid){
          	    writeHS(HScounter, element, true);
          	    break;
              }
              else if(element.uid == uid){
          	    writeHS(HScounter, element, true);
          	    break;
              }
              else if(HScounter < 9){
                writeHS(HScounter, element, false);

              }
            
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Back"))
        {


            gameObject.AddComponent<Create_Menue>();
            Destroy(this);
        }

        

        GUILayout.EndArea();

    }
    
    void writeHS(int rank, Score element, bool red){
            GUILayout.BeginHorizontal();
            
            if(red) GUI.skin.label.normal.textColor = Color.red;

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(rank.ToString()+". "+element.name);

            GUI.skin.label.alignment = TextAnchor.MiddleRight;
            GUILayout.Label(element.score);

            GUI.skin.label.normal.textColor = Color.white;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;

            GUILayout.EndHorizontal();
    }
}
