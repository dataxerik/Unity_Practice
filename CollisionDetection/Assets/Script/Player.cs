using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 6;

	float coinCount;
	Rigidbody myRigidbody;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 direction = input.normalized;
		velocity = direction * speed;
		print ("Coin count of " + coinCount);
	}

	void FixedUpdate(){
		myRigidbody.position += velocity * Time.deltaTime;
	}

	void OnTriggerEnter(Collider triggerCollider) {
		if(triggerCollider.gameObject.tag.Equals("Coin")){
			Destroy(triggerCollider.gameObject);
			coinCount++;
		}
	}
}
