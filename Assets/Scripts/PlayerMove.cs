using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float acceleration = 1000;
	public float maxVelocity = 50;

	public float jumpForce = 10;

	private Rigidbody playerCollider;

	private Vector3 storedVelocity = Vector3.zero;

	private bool hasJumped = false;

	void Awake () {
		playerCollider = gameObject.GetComponent<Rigidbody>();
	}
	
	void Update () {
	}
	public void Move (float forward, float strafe, float jump) {
		playerCollider.angularVelocity = Vector3.zero;
		playerCollider.AddForce(transform.forward * Time.deltaTime * acceleration * forward);
		playerCollider.AddForce(transform.right * Time.deltaTime * acceleration * strafe);
		if (playerCollider.velocity.magnitude > maxVelocity) {
			Vector3 normalVelcoity = playerCollider.velocity.normalized;
			playerCollider.velocity = normalVelcoity * maxVelocity;
		}
		if (jump > 0 && !hasJumped) {
			hasJumped = true;
			playerCollider.AddRelativeForce(transform.up * jumpForce * jump);
		}
	}

	public void Pause () {
		storedVelocity = playerCollider.velocity;
		playerCollider.velocity = Vector3.zero;
		playerCollider.useGravity = false;
	}

	public void Unpause () {
		playerCollider.velocity = storedVelocity;
		playerCollider.useGravity = true;
	}

	void OnCollisionEnter (Collision col) {
		hasJumped = false;
	}
	
	void OnCollisionExit (Collision col) {
		hasJumped = true;
	}
}
