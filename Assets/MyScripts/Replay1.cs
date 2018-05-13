//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Apple.ReplayKit;
//using UnityEngine.UI;
//public class Replay1 : MonoBehaviour
//{
//	public Image img;
//	bool Rec = false;
//	public dirtyTap dTap;

//    public void Start(){
//        img.enabled = false;
//    }

//	public void StartRecording()
//	{
//        Rec = true;
//	   	print("STARTING RECORDING");
//		print(ReplayKit.StartRecording());

//	}

//	public void StopRecording()
//	{
//        Rec = false;
//		ReplayKit.StopRecording();
//        StartCoroutine(checkRec());

//	}
//    IEnumerator recBounce;
//    public void toggleRec(){
//        if (!Rec){
//            recBounce = recB();
//            img.enabled = true;
//            StartCoroutine(recBounce);
//            StartRecording();
//        } else {
//            img.enabled = false;
//            StopCoroutine(recBounce);
//            StopRecording();
//        }
//    }

//    private IEnumerator recB()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(1f);
//            //dTap.onTap();
//        }

//    }

//    private IEnumerator checkRec()
//    {
//        while(!ReplayKit.recordingAvailable){
//            yield return new WaitForSeconds(.01f);
//        }
//        ReplayKit.Preview();
//    }
//}