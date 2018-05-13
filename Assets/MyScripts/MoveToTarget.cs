using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {
    public GameObject target;
    public float step = 0.01f;
    public float speed = .01f;
	// Use this for initialization
	void Start () {
        StartCoroutine(doLoco());
	}
    
    private IEnumerator doLoco()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, step);
            yield return new WaitForSeconds(speed);
        }
    }

    // Update is called once per frame
    void Update () {
        InstantlyTurn(target.transform.position);    
    }
	private void InstantlyTurn(Vector3 destination)
	{
		//When on target -> dont rotate!
		if ((destination - transform.position).magnitude < 0.1f) return;

		Vector3 direction = (destination - transform.position).normalized;
		Quaternion qDir = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 50);
	}
}
