using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controls the movement of the player.
public class PlayerMove : MonoBehaviour {

	/// The factor to apply to acceleration.
	public float acceleration = 1000;

	/// The maximum velocity the player may travel at.
	public float maxVelocity = 50;

	/// The force with which the player jumps.
	public float jumpForce = 500;

	/// The text to notify the player of a win in.
	public UnityEngine.UI.Text winTextLocation;

	/// The collision rigid body.
	private Rigidbody playerCollider;

	/// The velocity before pausing.
	private Vector3 storedVelocity = Vector3.zero;

	/// If the player has used their jump and needs to land before jumping 
	/// again.
	private bool hasJumped = false;

	/// Player collider component is retrieved in Awake as it is needed in
	/// the <c>Start</c> of another component and starts are unordered.
	void Awake () {
		playerCollider = gameObject.GetComponent<Rigidbody>();
	}
	
	/// Moves the player.
	public void Move (float forward, float strafe, float jump) {
		// Lock the angular velocity to prevent uncontrollable spinning.
		playerCollider.angularVelocity = Vector3.zero;
		// Add force proportional to acceleartion.
		playerCollider.AddForce(transform.forward * Time.deltaTime 
			* acceleration * forward);
		playerCollider.AddForce(transform.right * Time.deltaTime 
			* acceleration * strafe);
		// Clamp the velocity.
		if (playerCollider.velocity.magnitude > maxVelocity) {
			Vector3 normalVelcoity = playerCollider.velocity.normalized;
			playerCollider.velocity = normalVelcoity * maxVelocity;
		}
		// If the player is able to jump, then jump.
		if (jump > 0 && !hasJumped) {
			hasJumped = true;
			playerCollider.AddRelativeForce(transform.up * jumpForce * jump);
		}
	}

	/// Store the players velocity, stop them, and disable gravity.
	public void Pause () {
		storedVelocity = playerCollider.velocity;
		playerCollider.velocity = Vector3.zero;
		playerCollider.useGravity = false;
	}

	/// Turn on gravity and restore the players previous velocity.
	public void Unpause () {
		playerCollider.useGravity = true;
		playerCollider.velocity = storedVelocity;
	}

	/// Set the player back to their starting location.
	public void Reload () {
		playerCollider.velocity = Vector3.zero;
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.Euler(0, 90, 0);
	}

	/// Mark the player as able to jump again, and detect if the player has won.
	void OnCollisionEnter (Collision col) {
		hasJumped = false;
		if (col.gameObject.name.StartsWith("Winner") && 
			!winTextLocation.text.StartsWith("WINNER WINNER")) {

			winTextLocation.text = "WINNER WINNER at " + winTextLocation.text;
		}

	}
	
	/// Disable air jumps.
	void OnCollisionExit (Collision col) {
		hasJumped = true;
	}
}
