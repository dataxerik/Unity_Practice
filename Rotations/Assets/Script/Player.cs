using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float speed = 10f;

	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float z = Input.GetAxisRaw ("Vertical");
		transform.Translate (new Vector3(x, 0, z) * speed * Time.deltaTime);
	}
}
