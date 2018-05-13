using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtyTap : MonoBehaviour {

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
        while (transform.localScale.x > .9){
            Vector3 sca = transform.localScale;
            sca = sca - new Vector3(.05f, .05f, .05f);
            transform.localScale = sca;
            yield return new WaitForSeconds(.001f);
        }
		while (transform.localScale.x < 1.1)
		{
			Vector3 sca = transform.localScale;
			sca = sca + new Vector3(.05f, .05f, .05f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		} 
        while (transform.localScale.x > 1)
		{
			Vector3 sca = transform.localScale;
			sca = sca - new Vector3(.05f, .05f, .05f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		}
        isAnim = false;
    }
}
