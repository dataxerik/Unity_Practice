using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {
	public Transform sphereTransform;
	// Use this for initialization
	void Start () {
		sphereTransform.parent = transform;
		sphereTransform.localScale = Vector3.one * 2;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.eulerAngles += new Vector3 (0, 180 * Time.deltaTime, 0);
		//transform.eulerAngles += Vector3.up * 180 * Time.deltaTime; //Global Rotation
		//transform.Rotate(Vector3.up * 180 * Time.deltaTime); //Object space
		transform.Rotate(Vector3.up * 180 * Time.deltaTime, Space.World);
		transform.Translate (Vector3.forward * Time.deltaTime * 7, Space.World);

		if(Input.GetKeyDown(KeyCode.Space)) {
			sphereTransform.position = Vector3.zero;
			//sphereTransform.localPosition = Vector3.zero;
		}

	}
}
