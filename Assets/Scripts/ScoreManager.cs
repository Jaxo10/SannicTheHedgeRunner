using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public GameObject player;
    public GUIText textView;
    private PlatformerCharacter2D playerController;
    public float score;
    public int coins;

	void Start () {
        playerController = player.GetComponent<PlatformerCharacter2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float add = (playerController.speed) / 5F + coins / 20F;
        score += add;
        textView.text = "Score: " + ((int) score) + "\nMultiplier: " + ((int) (add * 10)) / 10F + "x";
	}
}
