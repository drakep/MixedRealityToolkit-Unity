using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blink : MonoBehaviour {

    public Image img;
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
            Color tmpC = img.color;
            tmpC.a = .1f;
            img.color = tmpC;
            StopCoroutine(quickStart);
            isAnim = false;
            return;
        }
        quickStart = quickAnim();
        StartCoroutine(quickStart);
    }

    private IEnumerator quickAnim()
    {
        isAnim = true;
        while (true){
            while (img.color.a > 0){
				Color tmpC = img.color;
                tmpC.a -= .01f;
				img.color = tmpC;
                yield return new WaitForSeconds(.01f);
            }
			while (img.color.a < 1)
			{
				Color tmpC = img.color;
                tmpC.a += .01f;
				img.color = tmpC;
				yield return new WaitForSeconds(.01f);
			}
        }

    }
}
