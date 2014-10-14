using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour {

	// Use this for initialization
	void OnBecameInvisible() {
	    GameObject.Destroy(gameObject);
	}
}
