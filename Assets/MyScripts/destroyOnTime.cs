using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTime : MonoBehaviour {
    public float secondsTilPop;
	// Use this for initialization
	void Start () {
		Destroy (gameObject,secondsTilPop);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
