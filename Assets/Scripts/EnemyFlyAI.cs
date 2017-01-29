using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyFlyAI : MonoBehaviour {

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
    private bool searchingForPlayer = false;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }

    void FixedUpdate() {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //TODO: always look at player?
        if (path == null) {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count) {
            if (pathIsEnded) {
                return;
            }
            //Debug.Log("end of path reached");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized; //Direction to next waypoint
        dir *= speed * Time.fixedDeltaTime;
        rb.AddForce(dir, fMode);
        if (rb.velocity.x < 0.0f) {
            transform.localScale = new Vector3(-3, 3, 1);
        }
        else if (rb.velocity.x > 0.0f) {
            transform.localScale = new Vector3(3, 3, 1);
        }


        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWayPointDistance) {
            currentWayPoint++;
            return;
        }
    }

    IEnumerator SearchForPlayer() {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if(sResult == null) {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        } else {
            searchingForPlayer = false;
            target = sResult.transform;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    public void OnPathComplete(Path p) {
        //Debug.Log("Got path, error if any is "+ p.error);
        if(!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }

    IEnumerator UpdatePath() {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
}
