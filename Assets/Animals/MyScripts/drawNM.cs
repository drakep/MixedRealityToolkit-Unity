using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class drawNM : MonoBehaviour
{
    bool drawMesh = false;
    public float range = 20;
    void Start()
    {
        coroutineMesh = drawNavMesh();
        StartCoroutine(coroutineMesh);
        drawMesh = !drawMesh;
    }
    public float lastPlaceTime = 0;

    // Update is called once per frame
    void Update()
    {

    }
    float totalMag;
    private Mesh mesh;
    IEnumerator drawNavMesh()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(5f);

            NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();
            transform.GetComponent<MeshFilter>().mesh = mesh = new Mesh();
			foreach (Vector2 i in triangles.vertices)
			{
                totalMag += i.magnitude;
			}
            Debug.Log(totalMag);
            mesh.vertices = triangles.vertices;
            mesh.triangles = triangles.indices;
        }
    }


    //

    //



    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public void ToggleMesh()
    {
        if (!drawMesh)
        {
            coroutineMesh = drawNavMesh();
            StartCoroutine(coroutineMesh);
            drawMesh = !drawMesh;
        }
        else
        {
            StopCoroutine(coroutineMesh);
            drawMesh = !drawMesh;
        }
    }
    private IEnumerator coroutineMesh;

	
	public float Area( Vector3[] mVertices)
	{
		Vector3 result = Vector3.zero;
		for (int p = mVertices.Length - 1, q = 0; q < mVertices.Length; p = q++)
		{
			result += Vector3.Cross(mVertices[q], mVertices[p]);
		}
		result *= 0.5f;
		return result.magnitude;
	}
}
