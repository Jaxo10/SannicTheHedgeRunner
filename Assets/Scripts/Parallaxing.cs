using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;	//array of all the back and foregrounds effected by parallaxing
    private float[] parallaxScales; //proportion of the cameras movement to move the backgrounds by
    public float smoothing;			//how smooth the parallax is going to be. need to be >0

    private Transform cam;			//reference to the main cameras transform
    private Vector3 previousCamPos;	//position of camera in previous frame

    //called before start(). good for references between scripts/objects
    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        //previous frame had the current frames camera position
        previousCamPos = cam.position;

        //assigning corresponding parallax scales
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;

        }
    }

    // Update is called once per frame
    void Update()
    {

        //for each background

        for (int i = 0; i < backgrounds.Length; i++)
        {
            //parallax is the opposite of the camera movement because the previous frame multiplied by the scale

            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position + teh parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the backgrounds current position with its target x pos
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current pos and target pos using "lerp" #derp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        //previous cam pos to current cam pos at end of frame (now)
        previousCamPos = cam.position;
    }
}
