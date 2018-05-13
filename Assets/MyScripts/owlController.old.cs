//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class owlController : MonoBehaviour {

//	public float m_Range = 25.0f;
//	NavMeshAgent m_agent;
//	private Animator animation;
//	float timeRemaining = 1;
//	Vector3 posHolding = new Vector3(10, 10, 10);


//	float force = 50;

//	GameObject destination;
//	bool pathing = true;

//	private GameObject target;
//	Transform final;
//	Vector3[] birdPath;
//	public GameObject bug;
//	List<GameObject> path = new List<GameObject>();
//	List<String> action = new List<string>();

//	GameObject navSphere;
//	bool moveToTarget = true;
//	public float moveStepSize;
//	public float moveStepWait;
//    public GameObject rotator;
//	private const float rotSpeed = 20f;
//	//public AudioClip hitManAudio;
//	//public AudioClip hitObsAudio;
//	//AudioSource audioSource;

//	void Start()
//	{
//		//audioSource = GetComponent<AudioSource>();
//		animation = gameObject.GetComponent<Animator>();
//		m_agent = GetComponent<NavMeshAgent>();
//		Vector3 point;
//		if (RandomPoint(transform.position, range, out point))
//		{
//			transform.position = point;
//		}
//		path.Add(rotator);
//		target = rotator;

//		//shitsAndgiggles(rotator.transform.position);
//		target = path[0];
//		//StartCoroutine(moveCheck());
//		StartCoroutine(doLoco());
//		m_agent.updateRotation = false;
       
//	}

//	void Update()
//	{
//        InstantlyTurn(target.transform.position);
//        SetAnimation();
//		if (distanceCheck(transform.position, target.transform.position) < .1f)
//		{
//			var tempObj = path[0];
//			path.Remove(tempObj);
//            if (tempObj != rotator) { 
//                Destroy(tempObj); 
//            }
//			if (path.Count > 0)
//			{
//				target = path[0];
//			}
//			else
//			{
//				pathing = false;
//                chooseDestination();
//			}
//		}
//	}

//    private void SetAnimation()
//    {
//        //throw new NotImplementedException();
//    }

//    void chooseDestination(){
//        Vector3 dest;
//        switch(UnityEngine.Random.Range(0,3)){
//            case 1 :
//                Debug.Log("case 1");
//				if (RandomPoint(transform.position, range, out dest))
//				{
//                    //m_agent.updatePosition = true;
//                    shitsAndgiggles(dest);
//				}
//                break;
//			case 2:
//				Debug.Log("case 2");
//				if (RandomPoint(transform.position, range, out dest))
//				{
//					//m_agent.updatePosition = true;
//                    dest = new Vector3(dest.x, UnityEngine.Random.Range(.3f, 2f), dest.z);
//                    shitsAndgiggles( dest );
//				}
//				break;
//			case 3:
//				Debug.Log("case 3");
//                path.Add(rotator);
//                break;
//            default:
//                break;
                
//        }
//    }
//	public float range = .1f;

//	bool RandomPoint(Vector3 center, float range, out Vector3 result)
//	{
//		for (int i = 0; i < 30; i++)
//		{
//			Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
//			NavMeshHit hit;
//			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
//			{
//				result = hit.position;
//				Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
//				return true;
//			}
//		}
//		result = Vector3.zero;
//		return false;
//	}
//	float distanceCheck(Vector3 firstPosition, Vector3 secondPosition)
//	{
//		Vector3 heading;
//		float distance;
//		float distanceSquared;
//		heading.x = firstPosition.x - secondPosition.x;
//		heading.y = firstPosition.y - secondPosition.y;
//		heading.z = firstPosition.z - secondPosition.z;

//		distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
//		distance = Mathf.Sqrt(distanceSquared);
//		return distance;
//	}
//	private IEnumerator backOffCaptureMode()
//	{
//		yield return new WaitForSeconds(5);
//	}

//	private void InstantlyTurn(Vector3 destination)
//	{
//		//When on target -> dont rotate!
//		if ((destination - transform.position).magnitude < 0.1f) return;

//		Vector3 direction = (destination - transform.position).normalized;
//		Quaternion qDir = Quaternion.LookRotation(direction);
//		transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);
//	}

//    private void shitsAndgiggles(Vector3 dest)
//	{
//		m_agent.destination = dest;
//        birdPath = m_agent.path.corners;
//	    //path.Clear();
//		foreach (Vector3 vect in birdPath)
//		{
//			var pathObj = Instantiate(bug, vect + new Vector3(0, .3f, 0), Quaternion.identity);
//			path.Add(pathObj);
//		}
//		target = path[0];
//	}

//	private IEnumerator moveCheck()
//	{
//		while (true)
//		{
//			yield return new WaitForSeconds(.001f);
//			if (m_agent.velocity.magnitude > 0)
//			{
//				animation.SetBool("Moving", true);
//			}
//			else
//			{
//				animation.SetBool("Moving", false);
//			}
//			posHolding = transform.position;
//		}
//	}
//	private IEnumerator doLoco()
//	{
//		while (moveToTarget)
//		{
//            Debug.Log("Moving");
//			transform.position = Vector3.Lerp(transform.position, target.transform.position, moveStepSize);
//			//if (distanceCheck(transform.position, target.transform.position) < .001)
//			//{
//			//	moveToTarget = false;
//			//	Destroy(navSphere);
//			//	navSphere = null;
//			//}
//			yield return new WaitForSeconds(moveStepWait);
//		}
//	}
//}


////navSphere = GameObject.Find("navSphere");
//   //     if (navSphere){
//   //         m_agent.updatePosition = false;
//   //         m_agent.updateRotation = false;
//   //         moveToTarget = true;

//			//StartCoroutine(doLoco());
//        //}

////void OnCollisionEnter(Collision collision)
////{
////  if (collision.gameObject.tag == "ClearObs" && gm.CaptureMode)
////  { // Add different conditions
////      Debug.Log("bumped - capture");
////      StartCoroutine(backOffCaptureMode());
////      alienCrate.SetActive(false);
////      if (gm.Aliens.Count != 0)
////      {
////          foreach (GameObject alien in gm.Aliens)
////          {
////              if (!alien.activeSelf)
////              {
////                  alien.transform.position = transform.position;
////                  alien.SetActive(true);
////              }
////          }
////      }
////      audioSource.PlayOneShot(hitManAudio);
////      Vector3 dir = collision.contacts[0].point - transform.position;
////      dir = -dir.normalized;
////      GetComponent<Rigidbody>().AddForce(dir * force);
////      m_agent.updatePosition = false;
////      gm.Bumped = (true);
////  }
////  if (collision.gameObject.tag == "ClearObs" && !gm.CaptureMode && !gm.Bumped && !gm.Rescued)
////  { // Add different conditions
////      Debug.Log("bumped - not capture");
////      m_agent.updatePosition = false;
////      Vector3 dir = collision.contacts[0].point - transform.position;
////      dir = -dir.normalized;
////      GetComponent<Rigidbody>().AddForce(dir * force);
////      gm.Bumped = (true);
////  }
////  if (collision.gameObject.tag == "Obs")
////  {
////      Destroy(collision.gameObject);
////      audioSource.PlayOneShot(hitObsAudio);
////  }
////  if (collision.gameObject.tag == "Zarzl" && !gm.CaptureMode)
////  {
////      gm.CaptureMode = true;
////      collision.gameObject.SetActive(false);
////      alienCrate.SetActive(true);
////      destination = landingZone;
////  }
////  if (collision.gameObject.tag == "Heli" && gm.CaptureMode)
////  {
////      Debug.Log("Heli + Capture");
////      gm.CaptureMode = false;
////      alienCrate.SetActive(false);
////      foreach (GameObject alien in gm.Aliens)
////      {
////          if (!alien.activeSelf)
////          {
////              gm.RemoveAlien(alien);
////              gm.AlienCount = gm.AlienCount - 1;
////              Destroy(alien);
////              return;
////          }
////      }
////      nextAlien();
////  }
////  if (collision.gameObject.tag == "Heli" && gm.Bumped)
////  {
////      Debug.Log("Heli + Bumped");
////      gm.Bumped = false;
////      gm.Rescued = true;
////      gameObject.transform.SetParent(UFO.transform);
////      gameObject.SetActive(false);
////  }
////}