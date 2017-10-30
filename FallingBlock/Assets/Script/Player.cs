using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 10f;

    float screenHalfWidthInWorldUnits;

	// Use this for initialization
	void Start () {
        float halfPlayerWidth = transform.localScale.x / 2;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        transform.Translate(movement.normalized * speed * Time.deltaTime);

        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
    }
}
