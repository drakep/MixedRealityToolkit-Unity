using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.XR.iOS;
using System.Collections;

namespace UnityEngine.XR.iOS
{
    public class placeAgent : MonoBehaviour
	{
		public int animalCount = 10;
		public List<GameObject> agentsToPlace;
		public List<GameObject> animals;
        void Start()
		{
			animals = new List<GameObject>();
        }

        private RaycastHit hit;
        float timer = 5f;
        void Update()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Vector3 dest;
                if (SampleNavPoint(Camera.main.transform.position, range, out dest))
                {
                        if (animals.Count > animalCount)
                        {
                            return;
                        }
                        var aniAgent = Instantiate(agentsToPlace[Random.Range(0, agentsToPlace.Count)], dest, Quaternion.identity);
                        aniAgent.transform.LookAt(Camera.main.transform);
                        aniAgent.name = "Animal";
                       animals.Add(aniAgent);
                 
                }
            }
        }

        public float range = 10f;
        bool SampleNavPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * 2f;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, .5f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
		bool FindTagNavPoint(Vector3 center, float range, out Vector3 result)
		{
			for (int i = 0; i < 30; i++)
			{
				Vector3 randomPoint = center + Random.insideUnitSphere * range;
				NavMeshHit hit;
				if (NavMesh.SamplePosition(randomPoint, out hit, .5f, NavMesh.AllAreas))
				{
					result = hit.position;
					Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
					return true;
				}
			}
			result = Vector3.zero;
			return false;
		}
       
		public void ClearAnimals()
		{
			while (animals.Count > 0)
			{
				foreach (GameObject gObj in animals)
				{
					Destroy(gObj);
					Debug.Log("Animal Destroyed");
				}
				animals.Clear();
			}
		}
    }
}

