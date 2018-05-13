using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class getArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(NavMesh.GetAreaFromName("Walkable"));
		NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();
        foreach(int i in triangles.areas){
            Debug.Log(i);
        }
	}
}
