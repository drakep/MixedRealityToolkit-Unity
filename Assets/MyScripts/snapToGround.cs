using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private GameObject groundTransform;
	// Update is called once per frame
	void Update () {
        groundTransform = GameObject.Find("GroundPlane");
        if (groundTransform){
            transform.position = groundTransform.transform.position;
        } else {
            Debug.Log(("Does Not Exist"));
        }
	}
}
