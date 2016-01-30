using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float groundSpeed;
	[SerializeField] private float angularSpeed;
	[SerializeField] private float jumpForceGround;
	[SerializeField] private float jumpForceAir;

	private Rigidbody rb;

	// Stored inputs from update
	private float strafe;
	private float walk;
	private float yaw;
	private bool isJumpDown;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	void Update () {
		// Get inputs
		strafe     = Input.GetAxis ("Strafe");
		walk       = Input.GetAxis ("Walk");
		yaw        = Input.GetAxis ("Yaw");
		isJumpDown = Input.GetButtonDown ("Jump");
	}

	void FixedUpdate() {
		// Move
		Vector3 strafeVec = transform.right * strafe * groundSpeed;
		Vector3 walkVec   = transform.forward * walk * groundSpeed;

		Vector3 groundVel = Vector3.ClampMagnitude (strafeVec + walkVec, groundSpeed);
		Vector3 currentGroundVel = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
		groundVel = Vector3.Lerp (currentGroundVel, groundVel, 0.1f);

		// Change planar velocity while preserving jump velocity
		rb.velocity = groundVel + new Vector3(0, rb.velocity.y, 0);

		// Turn
		float turnVel = Mathf.Lerp(rb.angularVelocity.y, yaw * angularSpeed, 0.2f);
		rb.angularVelocity = new Vector3 (0, turnVel, 0);

		// Jump
		if (isJumpDown && rb.velocity.y == 0) {
			// Jump from ground
			rb.AddRelativeForce (0, jumpForceGround, 0);
		} else if (isJumpDown) {
			// Flap frantically in air
			rb.AddRelativeForce (0, jumpForceAir, 0);
		}
	}
}
