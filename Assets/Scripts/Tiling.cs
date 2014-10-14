using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; //creation offset
    public bool hasARightBuddy = false;	//check for creation
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;	//if object isnt tileable
    private float spriteWidth = 0f;	//width of element to be created
    private Camera cam;
    private Transform myTransform;

    void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start() {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * myTransform.localScale.x;
        Debug.Log(spriteWidth);
    }

    // Update is called once per frame
    void Update() {
        //does it still need buddys? if no -> fuck off
        if(!hasALeftBuddy || !hasARightBuddy) {
            //calculate cameras extent of what the camera can see (in world coordinates)
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) - camHorizontalExtent;

            //check if edge is near enough for buddy to be created
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy) {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            } else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy) {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }
    //creates a buddy on the correct position
    void MakeNewBuddy(int rightOrLeft) {
        //calculating the position of new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //instantiating new buddy and storing him in a new variable

        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;


        //if not tileable: revert the x size of the object to avoid clipping
        if(reverseScale)
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0) newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        else newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
    }
}