using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.iOS;

public class simpleAgentController : MonoBehaviour {
	private Animator animation; 
    public float m_Range = 25.0f;
	NavMeshAgent m_agent;
	float timeRemaining = 1;
	float range = 3f;
    // Use this for initialization
    public placeAgent pAgent;

	void Enabled()
	{
		animation.SetBool("Moving", true);
	}

	void Start()
	{
        pAgent = FindObjectOfType<placeAgent>();
		m_agent = GetComponent<NavMeshAgent>();
		animation = gameObject.GetComponent<Animator>();
		m_agent.updateRotation = false;
        Vector3 point;
		if (RandomPoint(transform.position, .1f, out point))
		{
			transform.position = point;
		}
		if (RandomPoint(transform.position, 1f, out point))
		{
			m_agent.destination = point;
		}
		StartCoroutine(moveCheck());
	}

    internal void disableNav()
	{
        m_agent.updatePosition = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    void Update()
	{
        timeRemaining -= Time.deltaTime;
        colliderReset -= Time.deltaTime;
		InstantlyTurn(m_agent.destination);
		if (m_agent.pathPending || m_agent.remainingDistance > 0.1f || timeRemaining > 0)
			return;
        if (crying){
            crying = false;
            animation.SetBool("Crying", false);
        }
		timeRemaining = 5;
		Vector3 dest;
		range = 3f;
		if (RandomPoint(transform.position, range, out dest))
		{
			m_agent.destination = dest;
		}
	}

    internal void enableNav()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        m_agent.updatePosition = true;
    }

    bool crying = false;
	private IEnumerator moveCheck()
	{
		while (true)
		{
            yield return new WaitForEndOfFrame();
			if (m_agent.velocity.magnitude > .01)
			{
                if (!crying)
                {
                    animation.SetBool("Moving", true);
                }
			}
			else
			{
                if (!crying)
                {
                    animation.SetBool("Moving", false);
                }
			}
		}
	}

	bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
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
    float colliderReset = 2;
    void OnTriggerEnter(Collider collision)
	{
        if (colliderReset < 0)
        {
            if (collision.gameObject.tag == "Animal")
            {
                if (!uIt)
                {
                    m_agent.destination = transform.position - (transform.forward * .1f);
                    timeRemaining = 3f;
                    crying = true;
                    animation.SetBool("Crying", true);
                    colliderReset = 10;
                }
            }
        }
	}
	float distanceCheck(Vector3 firstPosition, Vector3 secondPosition)
	{

		Vector3 heading;
		float distance;
		float distanceSquared;
		heading.x = firstPosition.x - secondPosition.x;
		heading.y = firstPosition.y - secondPosition.y;
		heading.z = firstPosition.z - secondPosition.z;

		distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
		distance = Mathf.Sqrt(distanceSquared);
		return distance;
	}

	private void InstantlyTurn(Vector3 destination)
	{
		//When on target -> dont rotate!
		if ((destination - transform.position).magnitude < 0.1f) return;

		Vector3 direction = (destination - transform.position).normalized;
		Quaternion qDir = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 50);
	}

	void OnEnable()
	{
		StartCoroutine(moveCheck());
	}
    public bool uIt = false;
    GameObject tagBox;
   
	IEnumerator quickStart;
	bool isAnim = false;
	public void onTap()
	{
		if (isAnim)
		{
			transform.localScale = new Vector3(.2f, .2f, .2f);
			StopCoroutine(quickStart);
		}
		quickStart = quickAnim();
		StartCoroutine(quickStart);
	}

	private IEnumerator quickAnim()
	{
		isAnim = true;
		Debug.Log("Clicked");
		while (transform.localScale.x > .15)
		{
			Vector3 sca = transform.localScale;
			sca = sca - new Vector3(.01f, .01f, .01f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		}
		while (transform.localScale.x < .25)
		{
			Vector3 sca = transform.localScale;
			sca = sca + new Vector3(.01f, .01f, .01f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		}
		while (transform.localScale.x > .2)
		{
			Vector3 sca = transform.localScale;
			sca = sca - new Vector3(.01f, .01f, .01f);
			transform.localScale = sca;
			yield return new WaitForSeconds(.001f);
		}
		isAnim = false;
	}
}
