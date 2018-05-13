using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class animatedScroll : MonoBehaviour {
    float startPosY;
    GridLayoutGroup gL;
    public RectTransform rT;
	// Use this for initialization
	void Start () {
        //startPosY = transform.localPosition.y;
        //Debug.Log(startPosY);
        gL = transform.GetComponentInChildren < GridLayoutGroup > ();
        gL.padding.top = -3500;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    bool isUp = true;
    bool isDown = false;
    bool isGoingUp = false;
    bool isGoingDown = false;
    IEnumerator doGoUp;
    IEnumerator doGoDown;
    public void onPaintToggle(){
        if (isUp)
        {
            isUp = !isUp;
            doGoDown = goDown();
            if (isGoingUp){
                StopCoroutine(doGoUp);
            }
            StartCoroutine(doGoDown);
        } else
		{
            isUp = !isUp;
			doGoUp = goUp();
			if (isGoingDown)
			{
				StopCoroutine(doGoDown);
			}
			StartCoroutine(doGoUp);
		}
    }
    public void goUpTrig(){
        if (isUp){
            
        } else {
			isUp = !isUp;
			doGoUp = goUp();
			if (isGoingDown)
			{
				StopCoroutine(doGoDown);
			}
			StartCoroutine(doGoUp);
        }
    }
    private IEnumerator goUp()
    {
        
        isGoingUp = true;
        while (gL.padding.top > -3500)
		{
            yield return new WaitForSeconds(.01f);
            gL.enabled = false;
            int pos = gL.padding.top;
            pos -= 50;
			gL.padding.top = pos;
            gL.enabled = true;
		}
        isGoingUp = false;
    }

    private IEnumerator goDown()
    {
        isGoingDown = true;
		while (gL.padding.top < 10)
		{
			yield return new WaitForSeconds(.01f);
            gL.enabled = false;
			int pos = gL.padding.top;
			pos += 50;
			gL.padding.top = pos;
            gL.enabled = true;
		}
        Debug.Log(rT.localPosition);
        while (rT.localPosition.y > -1000){
			gL.enabled = false;
            Vector3 pos = rT.localPosition;
            pos.y -= 10;
			rT.localPosition = pos;
			gL.enabled = true;
			yield return new WaitForSeconds(.01f);
        }
        isGoingDown = false;
    }
}
