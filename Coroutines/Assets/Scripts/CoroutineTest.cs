using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour {
    public Transform[] waypoints;
    IEnumerator currentMoveCoroutine;

    // Use this for initialization
    void Start() {
        string[] messages = { "Welcome", "to", "this", "amazing", "game"};
        StartCoroutine(PrintMessage(messages, .5f));
        StartCoroutine(followPath());
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }
            currentMoveCoroutine = Move(Random.onUnitSphere * 5, 8);
            StartCoroutine(currentMoveCoroutine);
        }
    }

    IEnumerator followPath()
    {
        foreach(Transform waypoint in waypoints)
        {
            yield return StartCoroutine(Move(waypoint.position, 8));
            
        }
    }

    IEnumerator Move(Vector3 destination, float speed)
    {
        while(transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null; //waits for the next frame.
        }
    }
	
    IEnumerator PrintMessage(string[] messages, float delay)
    {
        foreach(string msg in messages)
        {
            print(msg);
            yield return new WaitForSeconds(delay); //waits for the coroutine to end.
        }
    }
}
