using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {
	public float speed = 7f;
	public Transform player;

	// Update is called once per frame
	void Update () {
		Vector3 movement = player.position - transform.position;

		if (movement.magnitude > 1.5f) {
			print (movement.magnitude);
			transform.Translate (movement.normalized * speed * Time.deltaTime);
		}
	}
}
