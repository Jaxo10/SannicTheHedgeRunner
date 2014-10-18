using UnityEngine;
using System.Collections;

public class CoinCollector : MonoBehaviour {

    public ScoreManager scoreManager;
    bool audioActive;
    Animator anim;
    Rigidbody2D coinBody;

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
            anim = collider.gameObject.GetComponent<Animator>();
            coinBody = collider.gameObject.GetComponent<Rigidbody2D>();
            anim.SetBool("Fade", true);
            coinBody.AddForce(new Vector2(0, 1000));
            ++scoreManager.coins;
        }
    }
}
