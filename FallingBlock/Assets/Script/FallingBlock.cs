using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    float halfScreenHeightInWordUnits;
    float speed = 10f;

    void Start()
    {
        halfScreenHeightInWordUnits = -Camera.main.orthographicSize - transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);

        if (transform.position.y < halfScreenHeightInWordUnits)
        {
            //print("Destroying, position y is " + transform.position.y);
            Destroy(gameObject);
        }
    }
}
