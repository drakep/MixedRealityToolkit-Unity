using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growTree : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
        maxScale = UnityEngine.Random.Range(maxScaleRange.x, maxScaleRange.y);
        growRate = UnityEngine.Random.Range(maxGrowRange.x, maxGrowRange.y);
        StartCoroutine(growDisTree());  
	}
    float maxScale;
    float growRate;
	public Vector2 maxScaleRange;
	public Vector2 maxGrowRange;
	private IEnumerator growDisTree()
	{
        float growScale;
        growScale = .0001f;//0.0051f - .01f * growRate;
		while (transform.localScale.x < maxScale)
		{
            yield return new WaitForSeconds(growScale);
			transform.localScale = transform.localScale + new Vector3(growScale,growScale,growScale);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
