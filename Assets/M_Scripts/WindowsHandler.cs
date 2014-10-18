using UnityEngine;
using System.Collections;

public class WindowsHandler : MonoBehaviour {

	public static void BackButtonPressed(){
		if (Application.loadedLevel == 0) {
						Application.Quit ();
				} else {
					GameObject MO = GameObject.Find ("AMenueObject");
					MO.AddComponent<Create_Menue> ();
					Time.timeScale = 0;
                    GameObject aVariable = GameObject.Find ("2D Character");
                    AudioSource audio = (AudioSource) aVariable.GetComponents<AudioSource>()[0];
                    if (audio.isPlaying) audio.Stop();
				}
	}

	public static void OnNavigatedFrom(){

		if (Application.loadedLevel == 1) {
						GameObject MO = GameObject.Find ("AMenueObject");
						MO.AddComponent<Create_Menue> ();

						Time.timeScale = 0;
                        GameObject aVariable = GameObject.Find("2D Character");
                        AudioSource audio = (AudioSource)aVariable.GetComponents<AudioSource>()[0];
                        if (audio.isPlaying) audio.Stop();
				}
	}	


}
