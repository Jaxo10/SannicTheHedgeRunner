using UnityEngine;
using System.Collections;

public class CoinCollector : MonoBehaviour {

    public ScoreManager scoreManager;
    bool audioActive;

    void Awake()
    {
        //initialize if sound should be played
        if (PlayerPrefs.HasKey("music"))
        {
            int mus = PlayerPrefs.GetInt("music");
            if (mus == 1) audioActive = true;
            else audioActive = false;
        }
        else audioActive = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Coin") {
            if(audioActive) GetComponents<AudioSource>()[1].Play();
            GameObject.Destroy(collider.gameObject);
            ++scoreManager.coins;
        }
    }
}
