using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{

    [SerializeField]
    float maxSpeed = 10F;				// The fastest the player can travel in the x axis.
    [SerializeField]
    float jumpForce = 400F;			// Amount of force added when the player jumps.	
    [SerializeField]
    LayerMask collideWithWhat;			// A mask determining what is ground to the character
    public float startingSpeed = 0.6F, speedIncrease = 0.002F;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Transform currentPlatform;

    public bool grounded = false;
    BoxCollider2D MyCollider;								// A position marking where to check if the player is grounded.
    float groundedRadius = .3f;							// Radius of the overlap circle to determine if grounded								// Whether or not the player is grounded.
    //float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
    Animator anim;										// Reference to the player's animator component.
    bool jump, jumping, slide;
    int slideFrame;
    public Vector2 scaleVector = new Vector2(0, 0.07F), moveVector = new Vector2(0, 0.04F);
    bool isDead;
    public bool audioActive;

    void Awake()
    {
        MyCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        speed = startingSpeed;
        isDead = false;

        //initialize if sound should be played
        if (PlayerPrefs.HasKey("music"))
        {
            int mus = PlayerPrefs.GetInt("music");
            if (mus == 1) audioActive = true;
            else audioActive = false;
        }
        else audioActive = true;
    }

    void Update()
    {
        if (speed < maxSpeed) speed += speedIncrease;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!jump) jump = (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2);
            if (!jumping) jumping = ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && touch.position.x > Screen.width / 2);
            if (!slide) slide = (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled && touch.position.x < Screen.width / 2);
        }
        else
        {
            if (!jump) jump = Input.GetButtonDown("Jump");
            jumping = Input.GetButton("Jump");
            slide = Input.GetKey(KeyCode.LeftControl);
            //Debug.Log("a" + slide);
        }
        if (transform.position.y < -11 && !isDead) dead();
    }

    void dead()
    {
        isDead = true;
        if (audio.isPlaying) audio.Stop();
        Time.timeScale = 0;
        GameObject MO = GameObject.Find("AMenueObject");
        MO.AddComponent<Game_Over> ();

    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(MyCollider.bounds.min, groundedRadius, collideWithWhat);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = speed;

        Vector2 min = MyCollider.bounds.min, max = MyCollider.bounds.max;
        if (!slide && anim.GetBool("Slide") && Physics2D.OverlapArea(new Vector2(min.x, max.y - 0.5F), new Vector2(max.x, max.y + 0.5F), collideWithWhat)) slide = true;
        if (grounded || !slide) anim.SetBool("Slide", slide);


        if (grounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
            rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
        }

        if (grounded && !slide)
        {
            audio.pitch = 0.5F + speed / 10 * 2.5F;
            if (!audio.isPlaying && audioActive) audio.Play();
        }
        else if (audio.isPlaying) audio.Stop();

        if (grounded && jump)
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        else if (jumping) rigidbody2D.gravityScale = 5 + speed / 2;
        else rigidbody2D.gravityScale = 8F + speed / 2;

        if (slide && slideFrame < 9)
        {
            MyCollider.size -= scaleVector;
            MyCollider.center += moveVector;
            ++slideFrame;
        }
        else if (!slide && slideFrame > 0)
        {
            MyCollider.size += scaleVector;
            MyCollider.center -= moveVector;
            --slideFrame;
        }

        //Debug.Log(slide);
        jump = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isDead) dead();
        else currentPlatform = collision.transform;
    }

}
