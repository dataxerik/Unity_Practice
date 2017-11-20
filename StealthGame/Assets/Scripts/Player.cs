using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public event System.Action OnReachEndOfLevel;
    public float speed = 5f;
    public float smootMoveTime = .1f;
    public float turnSpeed = 8;

    bool IsDisabled;
    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;

    Vector3 velocity;

    new Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpotted += Disable;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 inputDirection = Vector3.zero;

        if (!IsDisabled)
        {
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }

        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smootMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * speed * smoothInputMagnitude;
	}

    private void OnTriggerEnter(Collider hitCollider) 
    {
        if(hitCollider.tag == "Finish")
        {
            Disable();
            if(OnReachEndOfLevel != null)
            {
                OnReachEndOfLevel();
            }
        }
    }

    void Disable()
    {
        IsDisabled = true;
    }

    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Guard.OnGuardHasSpotted -= Disable;
    }
}
