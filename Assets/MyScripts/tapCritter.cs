using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapCritter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator quickStart;
    bool isAnim = false;
    public void onTap(){
        if (isAnim)
        {
            transform.localScale = new Vector3(1, 1, 1);
            StopCoroutine(quickStart);
        }
        quickStart = quickAnim();
        StartCoroutine(quickStart);
    }

    private IEnumerator quickAnim()
    {
        isAnim = true;
        Debug.Log("Clicked");
        while (transform.localScale.x > .15){
            Vector3 sca = transform.localScale;
            sca = sca - new Vector3(.01f, .01f, .01f);
            transform.localScale = sca;
            yield return new WaitForSeconds(.001f);
        }
		while (transform.localScale.x < .25)
		{
			Vector3 sca = transform.localScale;
			sca = sca + new Vector3(.01f, .01f, .01f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		} 
        while (transform.localScale.x > .2)
		{
			Vector3 sca = transform.localScale;
			sca = sca - new Vector3(.01f, .01f, .01f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		}
        isAnim = false;
    }
}
