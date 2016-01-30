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
		Vector3 groundVelocity = Vector3.ClampMagnitude(strafeVec + walkVec, groundSpeed);
		rb.velocity = groundVelocity + new Vector3(0, rb.velocity.y, 0);

		// Turn
		//rb.AddRelativeTorque (0, yaw * angularSpeed, 0);
		rb.angularVelocity = new Vector3 (0, yaw * angularSpeed, 0);

		// Jump
		if (isJumpDown && rb.velocity.y == 0) {
			rb.AddRelativeForce (0, jumpForceGround, 0);
		} else if (isJumpDown) {
			rb.AddRelativeForce (0, jumpForceAir, 0);
		}
	}
}
