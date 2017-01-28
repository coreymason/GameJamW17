using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    public Transform target;
    public float updateRate = 2f; //how many times each second update path
    private Seeker seeker;
    private Rigidbody2D rb;
    public Path path; //Calculated path
    public float speed = 300f; //Enemy speed per second
    public ForceMode2D fMode;
    [HideInInspector]
    public bool pathIsEnded = false;
    public float nextWayPointDistance = 3f; //max distance from AI to waypoint before continuing to next
    private int currentWayPoint = 0; //waypoint currently moving to
    
	void Start () {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target = null) {
            Debug.LogError("target not set/found. Get Rekt!");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
	}
	
	void FixedUpdate () {
        if (target == null) {
            //TODO: player search
            return;
        }

        //TODO: always look at player?
        if(path == null) {
            return;
        }
        if(currentWayPoint >= path.vectorPath.Count) {
            if(pathIsEnded) {
                return;
            }
            Debug.Log("end of path reached");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized; //Direction to next waypoint
        dir *= speed * Time.fixedDeltaTime;
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if(dist < nextWayPointDistance) {
            currentWayPoint++;
            return;
        }
    }

    public void OnPathComplete(Path p) {
        Debug.Log("Got path, error if any is "+ p.error);
        if(!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }

    IEnumerator UpdatePath() {
        if(target == null) {
            //TODO: player search
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
}
