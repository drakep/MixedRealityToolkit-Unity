using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
public class fadeStuff : MonoBehaviour {
    public CanvasGroup imgOne;
    public Text textOne;

    public GameObject lightPrefab;
	// Use this for initialization
	void Start () {
		ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration(UnityARAlignment.UnityARAlignmentGravity, UnityARPlaneDetection.Horizontal);
		Application.targetFrameRate = 60;
		sessionConfig.getPointCloudData = true;
		sessionConfig.enableLightEstimation = true;
		UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
		    StartCoroutine(doFad());
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    string[] openings = new string[] { "Loading Virtual Systems", "Finding Reality", "Merging with Reality", "Ready" };
    private IEnumerator doFad()
    {
        yield return new WaitForSeconds(2f);
		textOne.text = openings[1]; 
        yield return new WaitForSeconds(2f);
		textOne.text = openings[2]; 
        yield return new WaitForSeconds(2f);
		textOne.text = openings[3];
        while (imgOne.alpha > 0){
            imgOne.alpha -= .01f;
			
            yield return new WaitForSeconds(.01f);
        }
        Instantiate(lightPrefab, Camera.main.transform.position,Camera.main.transform.rotation);
        Destroy(gameObject);
    }
}
