using System;
using UnityEngine;
using UnityEngine.XR.iOS;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Linq;

public class PointCloudManipulator : MonoBehaviour
{
    public uint numPointsToShow = 100;
    public GameObject PointCloudPrefab = null;
    private List<GameObject> pointCloudObjects;
    private Vector3[] m_PointCloudData;
    //public Text text1, text2, text3, text4, text5;
    public List<Vector3> test;
	int count = 0;
	public List<GameObject> flowers;
    public List<GameObject> flowerPrefabs;
    public void Start()
    {
        flowerPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("GrowPrefabs"));
        //while(count < 99){
        //    count++;
        //    pointsCacheHold.Add(new Vector3(UnityEngine.Random.Range(0, 100),UnityEngine.Random.Range(0, 100),UnityEngine.Random.Range(0, 100)));    
        //}
        //Generate();
        StartCoroutine(prunePointCloudList());
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
        if (PointCloudPrefab != null)
        {
            pointCloudObjects = new List<GameObject>();
            for (int i = 0; i < numPointsToShow; i++)
            {
                pointCloudObjects.Add(Instantiate(PointCloudPrefab));
            }
      //      text3.text = pointCloudObjects.Count + "";
        }
    }

    private IEnumerator prunePointCloudList()
    {
        while (true)
        {
            string holdingCount = pointsCache.Count() + "";
            Vector3EqualityComparer vectorComp = new Vector3EqualityComparer();
            pointsCache = new List<Vector3>(new HashSet<Vector3>(pointsCache, vectorComp));// pointsCache.Distinct().ToList(); 
        //    text5.text = holdingCount + "  " + pointsCache.Count();
            string holding = "";
            foreach (Vector3 vect in pointsCache)
            {
                holding = holding + vect + " ";
            }
          //  text4.text = holding;
            if (pointsCacheHold.Count() > 500){
                pointsCacheHold.Clear();
            }
            pointsCacheHold.AddRange(pointsCache);
            pointsCacheHold = new List<Vector3>(new HashSet<Vector3>(pointsCacheHold, vectorComp));// pointsCache.Distinct().ToList(); 
//			text5.text = holdingCount + "  " + pointsCache.Count() + " " + pointsCacheHold.Count();
            if (pointsCacheHold.Count() > 100){
                Generate();
            }
            pointsCache.Clear();
			yield return new WaitForSeconds(1);
        }
    }

	Mesh mesh;
	private void Generate()
	{
        float destroyRamp = 0;
		foreach (Vector3 vect in pointsCacheHold)
		{
            Vector3 rotA = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
            var flowerObj = Instantiate(flowerPrefabs[UnityEngine.Random.Range(0, flowerPrefabs.Count)], vect, Quaternion.Euler(rotA));
            flowers.Add(flowerObj);
			if (flowers.Count() > 200)
			{
                destroyRamp += .1f;
				var flower = flowers[0];
				flowers.Remove(flowers[0]);
				Destroy(flower, destroyRamp);
			}
        }

  //      mesh = gameObject.GetComponent<MeshFilter>().mesh;
		//mesh.name = "Procedural Grid";

  //      mesh.vertices = pointsCacheHold.ToArray();

		//int[] triangles = new int[pointsCacheHold.Count()*3];
  //      int triCount = 0;
  //      int pCHcounter = 0;
  //      while (triCount < triangles.Length){
  //          triangles[triCount] = pCHcounter;
  //          pCHcounter++;
  //          if (pCHcounter > pointsCacheHold.Count()-1){
  //              pCHcounter = 0;
  //          }
  //          triCount++;
  //      }
		//mesh.triangles = triangles;
        pointsCacheHold.Clear();
	}

    List<Vector3> pointsCacheHold = new List<Vector3>();
    public void ARFrameUpdated(UnityARCamera camera)
    {
        m_PointCloudData = camera.pointCloudData;
        if (m_PointCloudData.Length > 0)
        {
  //          text1.text = "PCD Length : " + m_PointCloudData.Length + " First Entry " + m_PointCloudData[0];

        }
        else
        {
 //           text1.text = "No Point Cloud Data";
            //text4.text = "No Point Cloud Data";
        }
    }
    List<Vector3> pointsCache = new List<Vector3>();
    protected internal void LateUpdate()
    {
        if (PointCloudPrefab != null && m_PointCloudData != null)
        {
            //text2.text = m_PointCloudData.Length + " " + numPointsToShow;
            for (int count = 0; count < Math.Min(m_PointCloudData.Length, numPointsToShow); count++)
            {
                Vector4 vert = m_PointCloudData[count];
                //text3.text = vert + "";
                GameObject point = pointCloudObjects[count];
                //text3.text = text3.text + " " + point + " ";
                point.transform.position = new Vector3(vert.x, vert.y, vert.z);
                if (pointsCache.Count() < 500)
                {
                    pointsCache.Add(new Vector3(vert.x, vert.y, vert.z));
                }
                else
                {
                    pointsCache.Remove(pointsCache[0]);
                    pointsCache.Add(new Vector3(vert.x, vert.y, vert.z));
                }
            }
        }
		//while (count < 99)
		//{
		//	count++;
		//	pointsCache.Add(new Vector3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)));
		//}
        //count = 0;
    }
}


//test.Add(new Vector3(1,1,1));
//        test.Add(new Vector3(1, 2, 1));
//        test.Add(new Vector3(1, 2, 1));
//        test.Add(new Vector3(1, 1, 1));
//        test.Add(new Vector3(1, 1, 1));
//        test.Add(new Vector3(1, 3, 1));
//        test.Add(new Vector3(1, 0, 1));
//        string testH = "";
//        foreach (Vector3 vect in test)
//        {
//            testH = testH + vect + " ";
//        }
//        Debug.Log(testH + test.Count());
//        Vector3EqualityComparer vectorComp = new Vector3EqualityComparer();
//test = new List<Vector3>(new HashSet<Vector3>(test, vectorComp));
//testH = "";
//foreach (Vector3 vect in test)
//{
//    testH = testH + vect + " ";
//}
//Debug.Log(testH + test.Count());
