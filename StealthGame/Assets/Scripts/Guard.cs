using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    public Transform pathHolder;
    public float turnSpeed;
    public float walkSpeed;
    public float waitTimeAtWaypoint;

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;
    Transform player;
    Color originalSpotLightColor;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        viewAngle = spotLight.spotAngle;
        originalSpotLightColor = spotLight.color;

      
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < pathHolder.childCount; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(FollowPath2(waypoints));
    }

    private void Update()
    {
        if(CanSeePlayer())
        {
            spotLight.color = Color.red;
        } else
        {
            spotLight.color = originalSpotLightColor;
        }
    }

    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath2(Vector3[] points)
    {
        transform.position = points[0];
        int waypointPosition = 1;
        transform.LookAt(points[waypointPosition]);
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[waypointPosition], Time.deltaTime * walkSpeed);           
            if (transform.position == points[waypointPosition])
            {
                waypointPosition = (waypointPosition + 1) % points.Length;
                yield return new WaitForSeconds(waitTimeAtWaypoint);
                yield return StartCoroutine(TurnToFace(points[waypointPosition]));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLook = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLook.z, dirToLook.x) * Mathf.Rad2Deg;

        while(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            print(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)));
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    IEnumerator FollowPath(Vector3[] points)
    {
        transform.position = points[0];
        int waypointStartIndex = 1;
        while(true)
        {
            
            yield return StartCoroutine(Move(points[waypointStartIndex], walkSpeed));
            yield return new WaitForSeconds(waitTimeAtWaypoint);
            waypointStartIndex++;
            if (waypointStartIndex == points.Length)
            {
                waypointStartIndex = 0;
            }

        }
    }

    IEnumerator Move(Vector3 destination, float speed)
    {
        while(transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 preivousPosition = startPosition;
        foreach(Transform path in pathHolder)
        {
            Gizmos.DrawSphere(path.position, .3f);
            Gizmos.DrawLine(preivousPosition, path.position);
            preivousPosition = path.position;
        }
        Gizmos.DrawLine(preivousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
