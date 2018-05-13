//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.XR.iOS;

//public class birdController : MonoBehaviour
//{
//	Animator anim;
//	Rigidbody birdButt;
//	NavMeshAgent m_agent;
//	public GameObject target;
//	//// Use this for initialization
//	void Start()
//	{
//		m_agent = GetComponent<NavMeshAgent>();
//		m_agent.updatePosition = false;
//		m_agent.updateRotation = false;
//		m_agent.updateUpAxis = false;
//		anim = gameObject.GetComponent<Animator>();
//		transform.SetParent(null);
//		birdButt = GetComponent<Rigidbody>();
//		//handleStart();
//		currrentHurdle = target;
//		//Debug.Log(currrentHurdle.name);
//		targetTop = tempHurd.transform.Find("GameTargets");
//		start = targetTop.transform.Find("Start");
//		//Debug.Log(start.name);
//		end = targetTop.transform.Find("End");
//		target = start.gameObject;
//		gameObject.GetComponent<BoxCollider>().enabled = false;
//		gameObject.GetComponent<Rigidbody>().useGravity = false;
//		transform.localPosition += new Vector3(0, .5f, 0);
//	}
//	bool go = false;
//	float speed = 5f;
//	//// Update is called once per frame
//	bool notPlaced = true;
//	bool onNext = true;
//	bool triggerStart = true;
//	bool triggerEnd = true;
//	bool printCornersOnce = true;
//	float timer = 2;
//	void Update()
//	{
//		timer -= Time.deltaTime;
//		if (!go)
//		{
//			if (timer < 0)
//			{
//				if (Input.touchCount > 0)
//				{
//					go = true;
//					gm.gameStart(false);
//					gm.gameEnd(false);
//					gameObject.GetComponent<BoxCollider>().enabled = true;
//					gameObject.GetComponent<Rigidbody>().useGravity = true;
//				}
//			}
//		}
//		//Debug.Log(target.name);
//		if (go)
//		{
//			if (!pathing)
//			{
//				if (distanceCheck(transform.position, start.transform.position) < .01f && triggerStart)
//				{
//					target = end.gameObject;
//					notPlaced = true;
//					birdButt.velocity = Vector3.zero;
//					birdButt.angularVelocity = Vector3.zero;
//				}
//				if (distanceCheck(transform.position, target.transform.position) < .1f && target.name == "End" && notPlaced)
//				{
//					//Debug.Log("At End");
//					notPlaced = false;
//					go = false;
//					score += 1;
//					placeNext();
//				}
//			}
//			else
//			{
//				if (distanceCheck(transform.position, target.transform.position) < .1f)
//				{
//					var tempObj = path[0];
//					path.Remove(tempObj);
//					Destroy(tempObj);
//					if (path.Count > 0)
//					{
//						target = path[0];
//					}
//					else
//					{
//						pathing = false;
//						target = start.gameObject;
//					}
//				}
//			}
//			float step = speed * Time.deltaTime;
//			//transform.position = new Vector3(Vector3.MoveTowards(transform.position, target.transform.position, step).x, transform.position.y, Vector3.MoveTowards(transform.position, target.transform.position, step).z);

//			InstantlyTurn(target.transform.position);
//			transform.rotation = Quaternion.LookRotation(birdButt.velocity);
//			//InstantlyTurn(target.transform.position);
//			if (Input.touchCount > 0)
//			{
//				anim.SetTrigger("flap");
//				birdButt.AddForce(Vector3.up * 25);
//				birdButt.AddForce((target.transform.position - transform.position).normalized * 10);
//				//Debug.Log("Flap "+ (target.transform.position - transform.position).normalized);
//			}
//		}
//	}
//	int score;
//	private void onTheStart()
//	{
//		gm.createNext = true;
//		triggerStart = false;
//	}

//	float rotSpeed = 250;
//	private void InstantlyTurn(Vector3 destination)
//	{
//		if ((destination - transform.position).magnitude < 0.1f) return;
//		Vector3 direction = (destination - transform.position).normalized;
//		Quaternion qDir = Quaternion.LookRotation(direction);
//		transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);
//	}

//	Transform start;
//	Transform end;
//	Transform targetTop;
//	GameObject currrentHurdle;
//	public GameObject newHurdle;
//	public GameObject tempHurd;
//	public GameObject gameStart;
//	public GameObject gameEnd;
//	public GameObject startHurdle;
//	private void OnCollisionEnter(Collision collision)
//	{
//		Debug.Log("Collision");
//		if (collision.gameObject.tag == "Dead" && go)
//		{
//			timer = 2;
//			Destroy(tempHurd);
//			gm.gameEnd(true);
//			//score = 10;
//			gameEnd.GetComponent<TextMeshProUGUI>().text = "Game Over\n\nScore:" + score + "\n\nTap To Flap!";
//			Vector3 dest;
//			go = false;
//			birdButt.velocity = Vector3.zero;
//			birdButt.angularVelocity = Vector3.zero;
//			gameObject.GetComponent<BoxCollider>().enabled = false;
//			gameObject.GetComponent<Rigidbody>().useGravity = false;
//			transform.localPosition += new Vector3(0, .5f, 0);
//			if (SampleNavPoint(transform.position, range, out dest))
//			{
//				Debug.Log("Sample NP Found");
//				//dest = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
//				Vector3 tempRot = Quaternion.identity.eulerAngles;
//				Quaternion rotation = Quaternion.Euler(tempRot);
//				tempHurd = Instantiate(newHurdle, dest, rotation);
//			}
//			else
//			{
//				Vector3 tempRot = Quaternion.identity.eulerAngles;
//				Quaternion rotation = Quaternion.Euler(tempRot);
//				tempHurd = Instantiate(ball, new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)), Quaternion.identity);
//				Debug.Log("Nav Placement Failed");
//			}

//			targetTop = tempHurd.transform.Find("GameTargets");
//			start = targetTop.transform.Find("Start");
//			Debug.Log(start.name);
//			end = targetTop.transform.Find("End");
//			target = start.gameObject;
//			m_agent.destination = start.position;
//			birdPath = m_agent.path.corners;
//			shitsAndgiggles();
//			birdButt.velocity = Vector3.zero;
//			birdButt.angularVelocity = Vector3.zero;
//			InstantlyTurn(target.transform.position);
//			score = 0;
//		}
//	}
//	public void placeNext()
//	{
//		Debug.Log("Place Next" + notPlaced);
//		Vector3 dest;
//		bool placed = false;
//		int count = 0;
//		while (!placed)
//		{
//			count += 1;
//			if (SampleNavPoint(transform.position, range, out dest))
//			{
//				Debug.Log("Sample NP Found");
//				//dest = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
//				Vector3 tempRot = targetTop.rotation.eulerAngles + new Vector3(0, -90, 0);
//				Quaternion rotation = Quaternion.Euler(tempRot);
//				tempHurd = Instantiate(newHurdle, dest, rotation);
//				placed = true;
//			};
//			Debug.Log(placed);
//			//          if (count > 25) {
//			//              Vector3 tempRot = Quaternion.identity.eulerAngles;
//			//              Quaternion rotation = Quaternion.Euler(tempRot);
//			//              tempHurd = Instantiate(newHurdle, new Vector3 (0,-1,0), rotation);
//			//              Debug.Log ("Nav Placement Failed");
//			//              placed = true;
//			//          }
//		}
//		Destroy(targetTop.parent.gameObject);
//		targetTop = tempHurd.transform.Find("GameTargets");
//		start = targetTop.transform.Find("Start");
//		Debug.Log(start.name);
//		end = targetTop.transform.Find("End");
//		target = start.gameObject;
//		m_agent.destination = start.position;
//		birdPath = m_agent.path.corners;
//		shitsAndgiggles();
//		birdButt.velocity = Vector3.zero;
//		birdButt.angularVelocity = Vector3.zero;
//		InstantlyTurn(target.transform.position);
//		go = true;
//	}
//	public GameObject ball;
//	List<GameObject> path = new List<GameObject>();
//	private void shitsAndgiggles()
//	{
//		path.Clear();
//		foreach (Vector3 vect in birdPath)
//		{
//			var pathObj = Instantiate(ball, vect + new Vector3(0, .3f, 0), Quaternion.identity);
//			path.Add(pathObj);
//		}
//		target = path[0];
//		pathing = true;
//	}
//	bool pathing;
//	Vector3[] birdPath;
//	float distanceCheck(Vector3 firstPosition, Vector3 secondPosition)
//	{

//		Vector3 heading;
//		float distance;
//		float distanceSquared;
//		heading.x = firstPosition.x - secondPosition.x;
//		heading.y = 0;
//		heading.z = firstPosition.z - secondPosition.z;

//		distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
//		distance = Mathf.Sqrt(distanceSquared);
//		//Debug.Log(distance);
//		return distance;
//	}
//	public float range = 10f;
//	bool SampleNavPoint(Vector3 center, float range, out Vector3 result)
//	{
//		for (int i = 0; i < 30; i++)
//		{
//			Vector3 randomPoint = center + UnityEngine.Random.onUnitSphere * range;
//			NavMeshHit hit;
//			if (NavMesh.SamplePosition(randomPoint, out hit, .5f, NavMesh.AllAreas))
//			{
//				result = hit.position;
//				Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
//				return true;
//			}
//		}
//		result = Vector3.zero;
//		return false;
//	}
//}
////InstantlyTurn(m_agent.destination);
////if (Quaternion.Angle(Quaternion.LookRotation(birdButt.velocity), transform.rotation) > 10)
////{
////    angle = Quaternion.Angle(Quaternion.LookRotation(birdButt.velocity), transform.rotation);
////    angle -= rotSpeed * Time.deltaTime;
////}
////else {
////    angle = Quaternion.Angle(Quaternion.LookRotation(birdButt.velocity), transform.rotation);
////    angle += rotSpeed * Time.deltaTime;
////}
////var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
////transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * damping);
////Vector3 tempRot = transform.rotation.eulerAngles;
////tempRot = new Vector3(tempRot.x, 0, 0);
////transform.rotation = Quaternion.Euler(tempRot);